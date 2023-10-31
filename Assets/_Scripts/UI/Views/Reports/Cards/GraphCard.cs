using System;
using System.Collections.Generic;
using System.Linq;
using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace UI.Views.Reports.Cards
{
    public class GraphCard : ReportCard
    {
        private List<DateTime> _totalMcpFillLevelTimestamps;
        private List<float> _totalMcpFillLevelValues;
        private List<DateTime> _mcpEmptiedTimestamps;

        private List<TextElement> _totalMcpFillLevelLabels;
        private List<TextElement> _mcpEmptiedLabels;
        private List<TextElement> _timestamps;

        private const int VALUE_COUNT = 8;
        private const int HOUR_COUNT = 24;
        private static readonly Padding GraphPadding = new(96, 96, 64, 128);
        private static readonly Padding LabelPadding = new(64, 64, 64, 128);
        private static readonly Color TotalMcpFillLevelColor = new(121f / 255, 225f / 255, 153f / 255, 1);
        private static readonly Color McpEmptiedColor = new(90f / 255, 145f / 255, 254f / 255, 1);
        private Rect _graphRect;

        public GraphCard() : base(nameof(GraphCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/GraphCard"));

            GenerateMockData();
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

        private void GenerateMockData()
        {
            _totalMcpFillLevelTimestamps = new List<DateTime>();
            _totalMcpFillLevelValues = new List<float>();
            _mcpEmptiedTimestamps = new List<DateTime>();

            for (int i = 0; i < HOUR_COUNT; i++)
            {
                _totalMcpFillLevelTimestamps.Add(DateTime.Now.AddHours(i - HOUR_COUNT));
                _totalMcpFillLevelValues.Add(Random.Range(0f, 1f));
                var emptyTimes = Random.Range(1, 9);
                for (int j = 0; j < emptyTimes; j++)
                {
                    _mcpEmptiedTimestamps.Add(DateTime.Now.AddHours(i - HOUR_COUNT));
                }
            }
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            _totalMcpFillLevelTimestamps = response.TotalMcpFillLevelTimestamps;
            _totalMcpFillLevelValues = response.TotalMcpFillLevelValues;
            _mcpEmptiedTimestamps = response.McpEmptiedTimestamps;
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
                    text = $"{i}%",
                    name = "McpFillLevelLabel"
                };
                _totalMcpFillLevelLabels.Add(mcpFillLevelLabel);
                Add(mcpFillLevelLabel);

                var mcpEmptiedLabel = new TextElement
                {
                    text = $"{i}",
                    name = "McpEmptiedLabel"
                };
                _mcpEmptiedLabels.Add(mcpEmptiedLabel);
                Add(mcpEmptiedLabel);
            }

            for (var i = 0; i < HOUR_COUNT; i++)
            {
                var timestamp = new TextElement
                {
                    text = $"{i}%",
                    name = "TimestampLabel"
                };
                _timestamps.Add(timestamp);
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

            var mcpFillLevelTitle = new TextElement { text = "Hourly aggregated MCPs fill level", name = "McpFillLevelTitle" };
            legendContainer.Add(mcpFillLevelTitle);

            var mcpEmptiedLegendIcon = new VisualElement { name = "McpEmptiedLegend" };
            mcpEmptiedLegendIcon.AddToClassList("legend-icon");
            mcpEmptiedLegendIcon.style.backgroundColor = McpEmptiedColor;
            legendContainer.Add(mcpEmptiedLegendIcon);

            var mcpEmptiedTitle = new TextElement { text = "Hourly MCPs Emptied", name = "McpEmptiedTitle" };
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
            painter.lineJoin = LineJoin.Round;
            painter.lineCap = LineCap.Round;
            painter.lineWidth = 1;

            var startX = GraphPadding.Left;
            var endX = resolvedStyle.width - GraphPadding.Right;
            var graphHeight = resolvedStyle.height - (LabelPadding.Top + LabelPadding.Bottom);
            for (var i = 0; i < VALUE_COUNT; i++)
            {
                painter.strokeColor = Color.gray;
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

                var mcpEmptiedLabel = _mcpEmptiedLabels[VALUE_COUNT - 1 - i];
                var mcpEmptiedLabelPosition = new Vector2(endX, graphHeight / (VALUE_COUNT - 1) * i + LabelPadding.Top);
                mcpEmptiedLabel.style.left = mcpEmptiedLabelPosition.x - size.x / 2;
                mcpEmptiedLabel.style.right = resolvedStyle.width - (mcpEmptiedLabelPosition.x + size.x / 2);
                mcpEmptiedLabel.style.top = mcpEmptiedLabelPosition.y - size.y / 2;
                mcpEmptiedLabel.style.bottom = resolvedStyle.height - (mcpEmptiedLabelPosition.y + size.y / 2);
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

                timestamp.text = $"{i}:00";
            }
        }

        private void DrawMcpFillLevelGraph(Painter2D painter)
        {
            painter.lineCap = LineCap.Round;
            painter.lineJoin = LineJoin.Round;
            painter.lineWidth = 2;
            painter.fillColor = TotalMcpFillLevelColor;
            painter.strokeColor = TotalMcpFillLevelColor;

            painter.BeginPath();
            painter.MoveTo(_graphRect.position + new Vector2(0, _graphRect.height));
            for (int i = 0; i < _totalMcpFillLevelValues.Count; i++)
            {
                var point = GetGraphPoint(
                    _totalMcpFillLevelValues[i],
                    0,
                    _totalMcpFillLevelValues.Max(),
                    _totalMcpFillLevelTimestamps[i],
                    DateTime.Now.AddHours(-24),
                    DateTime.Now);
                painter.LineTo(point);
            }

            painter.LineTo(_graphRect.position + new Vector2(_graphRect.width, _graphRect.height));
            painter.Fill();
            painter.Stroke();
        }

        private void DrawMcpEmptiedGraph(Painter2D painter)
        {
            var _mcpEmptiedPerHour = new Dictionary<int, int>();
            foreach (var emptyTimestamp in _mcpEmptiedTimestamps)
            {
                var offsetHours = (DateTime.Now.Hour - emptyTimestamp.Hour);
                if (_mcpEmptiedPerHour.ContainsKey(offsetHours))
                {
                    _mcpEmptiedPerHour[offsetHours]++;
                }
                else
                {
                    _mcpEmptiedPerHour.Add(offsetHours, 1);
                }
            }

            painter.lineCap = LineCap.Round;
            painter.lineJoin = LineJoin.Round;
            painter.lineWidth = 3;
            painter.strokeColor = McpEmptiedColor;

            painter.BeginPath();
            painter.MoveTo(_graphRect.position + new Vector2(0, _graphRect.height));
            for (int i = HOUR_COUNT - 1; i >= 0; i--)
            {
                if (!_mcpEmptiedPerHour.ContainsKey(i)) continue;

                var point = GetGraphPoint(_mcpEmptiedPerHour[i], 0, 8, DateTime.Now.AddHours(-i), DateTime.Now.AddHours(-24), DateTime.Now);
                painter.LineTo(point);
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
            var y = minY + (maxY - minY) / (maxValue - minValue) * (value - minValue);
            return new Vector2((float)x, y);
        }
    }
}