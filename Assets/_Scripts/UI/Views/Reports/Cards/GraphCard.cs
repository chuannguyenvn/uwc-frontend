using System;
using System.Collections.Generic;
using System.Linq;
using Commons.Communications.Reports;
using LocalizationNS;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;
using Random = UnityEngine.Random;

namespace UI.Views.Reports.Cards
{
    public class GraphCard : ReportCard
    {
        private List<DateTime> _totalMcpFillLevelTimestamps = new();
        private List<float> _totalMcpFillLevelValues = new();
        private List<DateTime> _mcpEmptiedTimestamps = new();

        private List<TextElement> _totalMcpFillLevelLabels = new();
        private List<TextElement> _mcpEmptiedLabels = new();
        private List<TextElement> _timestamps = new();

        private const int VALUE_COUNT = 9;
        private const int HOUR_COUNT = 25;
        private static int MAX_EMPTIED_PER_HOUR = 4;
        private static readonly Padding GraphPadding = new(96, 96, 64, 128);
        private static readonly Padding LabelPadding = new(64, 64, 64, 128);
        private static readonly Color GraphLineColor = new(217f / 255, 217f / 255, 217f / 255, 1);
        private static readonly Color TotalMcpFillLevelColor = new(121f / 255, 225f / 255, 153f / 255, 1);
        private static readonly Color McpEmptiedColor = new(90f / 255, 145f / 255, 254f / 255, 1);
        private Rect _graphRect;

        public GraphCard() : base(nameof(GraphCard))
        {
            ConfigureUss(nameof(GraphCard));

            CreateLabels();
            CreateLegends();

            generateVisualContent += GenerateVisualContent;
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            ModifyGraphValues();
            ModifyTimestamps();
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            _totalMcpFillLevelTimestamps = response.TotalMcpFillLevelTimestamps;
            _totalMcpFillLevelValues = response.TotalMcpFillLevelValues;
            _mcpEmptiedTimestamps = response.McpEmptiedTimestamps;

            schedule.Execute(() =>
            {
                ModifyGraphValues();
                ModifyTimestamps();
            });

            MarkDirtyRepaint();
        }

        private void CreateLabels()
        {
            _totalMcpFillLevelLabels = new List<TextElement>();
            _mcpEmptiedLabels = new List<TextElement>();
            _timestamps = new List<TextElement>();

            for (var i = 0; i < VALUE_COUNT; i++)
            {
                var mcpFillLevelLabel = new TextElement
                {
                    name = "McpFillLevelLabel"
                };
                _totalMcpFillLevelLabels.Add(mcpFillLevelLabel);
                mcpFillLevelLabel.AddToClassList("grey-text");
                mcpFillLevelLabel.AddToClassList("graph-text");
                Add(mcpFillLevelLabel);

                var mcpEmptiedLabel = new TextElement
                {
                    name = "McpEmptiedLabel"
                };
                _mcpEmptiedLabels.Add(mcpEmptiedLabel);
                mcpEmptiedLabel.AddToClassList("grey-text");
                mcpEmptiedLabel.AddToClassList("graph-text");
                Add(mcpEmptiedLabel);
            }

            for (var i = 0; i < HOUR_COUNT; i++)
            {
                var timestamp = new TextElement
                {
                    name = "TimestampLabel"
                };
                _timestamps.Add(timestamp);
                timestamp.AddToClassList("grey-text");
                timestamp.AddToClassList("graph-text");
                Add(timestamp);
            }
        }

        private void CreateLegends()
        {
            var legendContainer = new VisualElement { name = "LegendContainer" };
            Add(legendContainer);

            var mcpFillLevelLegendIcon = new VisualElement { name = "McpFillLevelLegend" };
            mcpFillLevelLegendIcon.AddToClassList("legend-icon");
            mcpFillLevelLegendIcon.style.backgroundColor = TotalMcpFillLevelColor;
            legendContainer.Add(mcpFillLevelLegendIcon);

            var mcpFillLevelTitle = new TextElement
                { text = Localization.GetSentence(Sentence.ReportingView.HOURLY_AGGREGATED_MCPS_FILL_LEVEL), name = "McpFillLevelTitle" };
            mcpFillLevelTitle.AddToClassList("sub-text");
            mcpFillLevelTitle.AddToClassList("grey-text");
            legendContainer.Add(mcpFillLevelTitle);

            var mcpEmptiedLegendIcon = new VisualElement { name = "McpEmptiedLegend" };
            mcpEmptiedLegendIcon.AddToClassList("legend-icon");
            mcpEmptiedLegendIcon.style.backgroundColor = McpEmptiedColor;
            legendContainer.Add(mcpEmptiedLegendIcon);

            var mcpEmptiedTitle = new TextElement
                { text = Localization.GetSentence(Sentence.ReportingView.HOURLY_MCPS_EMPTIED), name = "McpEmptiedTitle" };
            mcpEmptiedTitle.AddToClassList("sub-text");
            mcpEmptiedTitle.AddToClassList("grey-text");
            legendContainer.Add(mcpEmptiedTitle);
        }

        private void GenerateVisualContent(MeshGenerationContext ctx)
        {
            var painter = ctx.painter2D;

            DrawGraphLines(painter);
            DrawMcpFillLevelGraph(painter);
            DrawMcpEmptiedGraph(painter);
        }

        private void DrawGraphLines(Painter2D painter)
        {
            painter.lineWidth = 1;
            painter.strokeColor = GraphLineColor;

            var startX = GraphPadding.Left;
            var endX = resolvedStyle.width - GraphPadding.Right;
            var graphHeight = resolvedStyle.height - (LabelPadding.Top + LabelPadding.Bottom);
            for (var i = 0; i < VALUE_COUNT - 1; i++)
            {
                painter.BeginPath();
                painter.MoveTo(new Vector2(startX, graphHeight / (VALUE_COUNT - 1) * i + LabelPadding.Top));
                painter.LineTo(new Vector2(endX, graphHeight / (VALUE_COUNT - 1) * i + LabelPadding.Top));
                painter.Stroke();
            }

            _graphRect = new Rect(startX, LabelPadding.Top, endX - startX, graphHeight);
        }

        private void ModifyGraphValues()
        {
            var startX = LabelPadding.Left;
            var endX = resolvedStyle.width - LabelPadding.Right;
            var graphHeight = resolvedStyle.height - (LabelPadding.Top + LabelPadding.Bottom);
            var size = new Vector2(50, 20);

            for (var i = 0; i < VALUE_COUNT; i++)
            {
                var mcpFillLevelLabel = _totalMcpFillLevelLabels[VALUE_COUNT - 1 - i];
                var mcpFillLevelLabelPosition = new Vector2(startX, graphHeight / (VALUE_COUNT - 1) * i + LabelPadding.Top);
                mcpFillLevelLabel.style.left = mcpFillLevelLabelPosition.x - size.x / 2;
                mcpFillLevelLabel.style.right = resolvedStyle.width - (mcpFillLevelLabelPosition.x + size.x / 2);
                mcpFillLevelLabel.style.top = mcpFillLevelLabelPosition.y - size.y / 2;
                mcpFillLevelLabel.style.bottom = resolvedStyle.height - (mcpFillLevelLabelPosition.y + size.y / 2);
                mcpFillLevelLabel.text = $"{(VALUE_COUNT - 1 - i) * 100 / (VALUE_COUNT - 1)}%";

                var mcpEmptiedLabel = _mcpEmptiedLabels[VALUE_COUNT - 1 - i];
                var mcpEmptiedLabelPosition = new Vector2(endX, graphHeight / (VALUE_COUNT - 1) * i + LabelPadding.Top);
                mcpEmptiedLabel.style.left = mcpEmptiedLabelPosition.x - size.x / 2;
                mcpEmptiedLabel.style.right = resolvedStyle.width - (mcpEmptiedLabelPosition.x + size.x / 2);
                mcpEmptiedLabel.style.top = mcpEmptiedLabelPosition.y - size.y / 2;
                mcpEmptiedLabel.style.bottom = resolvedStyle.height - (mcpEmptiedLabelPosition.y + size.y / 2);
                mcpEmptiedLabel.text = $"{(VALUE_COUNT - 1 - i) * MAX_EMPTIED_PER_HOUR / (VALUE_COUNT - 1)}";

                if (i % 2 == 1)
                {
                    mcpFillLevelLabel.style.display = mcpEmptiedLabel.style.display = DisplayStyle.None;
                }
            }
        }

        private void ModifyTimestamps()
        {
            var startX = GraphPadding.Left;
            var y = resolvedStyle.height - GraphPadding.Bottom;
            var graphWidth = resolvedStyle.width - (GraphPadding.Left + GraphPadding.Right);
            var size = new Vector2(50, 20);

            for (var i = 0; i < HOUR_COUNT; i++)
            {
                var timestamp = _timestamps[i];

                var position = new Vector2(graphWidth / (HOUR_COUNT - 1) * i + startX, y + 20);
                timestamp.style.left = position.x - size.x / 2;
                timestamp.style.right = resolvedStyle.width - (position.x + size.x / 2);
                timestamp.style.top = position.y - size.y / 2;
                timestamp.style.bottom = resolvedStyle.height - (position.y + size.y / 2);

                timestamp.text = $"{DateTime.Now.AddHours(-HOUR_COUNT + i + 1).Hour}:00";
            }
        }

        private void DrawMcpFillLevelGraph(Painter2D painter)
        {
            painter.fillColor = TotalMcpFillLevelColor;

            painter.BeginPath();
            painter.MoveTo(_graphRect.position + new Vector2(0, _graphRect.height));

            var first = true;
            var firstPoint = new Vector2();
            var lastPoint = new Vector2();

            var hourlyAveragedMcpFillLevel = new Dictionary<int, float>();
            var hourlyAveragedMcpFillLevelCount = new Dictionary<int, int>();
            foreach (var fillLevelTimestamp in _totalMcpFillLevelTimestamps)
            {
                var offsetHours = (DateTime.UtcNow - fillLevelTimestamp).Hours;
                if (hourlyAveragedMcpFillLevel.ContainsKey(offsetHours))
                {
                    hourlyAveragedMcpFillLevel[offsetHours] += _totalMcpFillLevelValues[_totalMcpFillLevelTimestamps.IndexOf(fillLevelTimestamp)];
                    hourlyAveragedMcpFillLevelCount[offsetHours]++;
                }
                else
                {
                    hourlyAveragedMcpFillLevel.Add(offsetHours, _totalMcpFillLevelValues[_totalMcpFillLevelTimestamps.IndexOf(fillLevelTimestamp)]);
                    hourlyAveragedMcpFillLevelCount.Add(offsetHours, 1);
                }
            }

            for (var i = 0; i < hourlyAveragedMcpFillLevel.Count; i++)
            {
                var hour = hourlyAveragedMcpFillLevel.ToList()[i].Key;
                hourlyAveragedMcpFillLevel[hour] /= hourlyAveragedMcpFillLevelCount[hour];
            }

            var minHour = new DateTime(DateTime.UtcNow.AddHours(-24).Year, DateTime.UtcNow.AddHours(-24).Month, DateTime.UtcNow.AddHours(-24).Day,
                DateTime.UtcNow.AddHours(-24).Hour, 0, 0);
            var maxHour = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, 0, 0);
            for (int i = 0; i < hourlyAveragedMcpFillLevel.Count; i++)
            {
                var currentHour = new DateTime(DateTime.UtcNow.AddHours(-hourlyAveragedMcpFillLevel.ToList()[i].Key).Year, DateTime.UtcNow.AddHours(-hourlyAveragedMcpFillLevel.ToList()[i].Key).Month,
                    DateTime.UtcNow.AddHours(-hourlyAveragedMcpFillLevel.ToList()[i].Key).Day, DateTime.UtcNow.AddHours(-hourlyAveragedMcpFillLevel.ToList()[i].Key).Hour, 0, 0);
                var point = GetGraphPoint(
                    hourlyAveragedMcpFillLevel.ToList()[i].Value,
                    0,
                    1,
                    currentHour,
                    minHour,
                    maxHour);

                Debug.Log(point);

                if (first)
                {
                    painter.MoveTo(point);
                    firstPoint = point;
                }
                else painter.LineTo(point);

                first = false;

                lastPoint = point;
            }

            painter.LineTo(new Vector2(lastPoint.x, _graphRect.height + _graphRect.position.y));
            painter.LineTo(new Vector2(firstPoint.x, _graphRect.height + _graphRect.position.y));
            painter.Fill();
        }

        private void DrawMcpEmptiedGraph(Painter2D painter)
        {
            var mcpEmptiedPerHour = new Dictionary<int, int>();
            foreach (var emptyTimestamp in _mcpEmptiedTimestamps)
            {
                var offsetHours = (DateTime.UtcNow - emptyTimestamp).Hours;
                if (mcpEmptiedPerHour.ContainsKey(offsetHours))
                {
                    mcpEmptiedPerHour[offsetHours]++;
                }
                else
                {
                    mcpEmptiedPerHour.Add(offsetHours, 1);
                }
            }
            
            var maxValues = new List<int>() { 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768 };
            var maxValue = mcpEmptiedPerHour.Any() ? maxValues.First(m => m >= mcpEmptiedPerHour.Values.Max()) : 4;
            MAX_EMPTIED_PER_HOUR = maxValue;

            painter.lineCap = LineCap.Round;
            painter.lineJoin = LineJoin.Bevel;
            painter.lineWidth = 3;
            painter.strokeColor = McpEmptiedColor;

            painter.BeginPath();
            painter.MoveTo(_graphRect.position + new Vector2(0, _graphRect.height));

            var first = true;

            var minHour = new DateTime(DateTime.UtcNow.AddHours(-24).Year, DateTime.UtcNow.AddHours(-24).Month, DateTime.UtcNow.AddHours(-24).Day,
                DateTime.UtcNow.AddHours(-24).Hour, 0, 0);
            var maxHour = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, 0, 0);
            for (int i = HOUR_COUNT - 1; i >= 0; i--)
            {
                var value = 0;
                if (mcpEmptiedPerHour.ContainsKey(i)) value = mcpEmptiedPerHour[i];

                var currentHour = new DateTime(DateTime.UtcNow.AddHours(-i).Year, DateTime.UtcNow.AddHours(-i).Month,
                    DateTime.UtcNow.AddHours(-i).Day, DateTime.UtcNow.AddHours(-i).Hour, 0, 0);
                var point = GetGraphPoint(value, 0, maxValue, currentHour,
                    minHour,
                    maxHour);

                if (first) painter.MoveTo(point);
                else painter.LineTo(point);

                first = false;
            }

            painter.Stroke();
        }

        private Vector2 GetGraphPoint(float value, float minValue, float maxValue, DateTime hour, DateTime minHour, DateTime maxHour)
        {
            var minX = _graphRect.position.x;
            var maxX = _graphRect.position.x + _graphRect.size.x;
            var minY = _graphRect.position.y;
            var maxY = _graphRect.position.y + _graphRect.size.y;

            var x = minX + (maxX - minX) / (maxHour - minHour).TotalHours * (hour - minHour).TotalHours;
            var y = minY + (maxY - minY) * (1 - (value - minValue) / (maxValue - minValue));
            return new Vector2((float)x, y);
        }
    }
}