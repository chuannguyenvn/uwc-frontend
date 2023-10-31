using System;
using System.Collections.Generic;
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
        private Rect _graphRect;

        public GraphCard() : base(nameof(GraphCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/GraphCard"));
            GenerateMockData();
            CreateLabels();
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

            for (int i = 0; i < 24; i++)
            {
                _totalMcpFillLevelTimestamps.Add(DateTime.Now.AddHours(24 - i));
                _totalMcpFillLevelValues.Add(Random.Range(0f, 1f));
                var emptyTimes = Random.Range(1, 9);
                for (int j = 0; j < emptyTimes; j++)
                {
                    _mcpEmptiedTimestamps.Add(DateTime.Now.AddHours(24 - i));
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

        private void GenerateVisualContent(MeshGenerationContext ctx)
        {
            var painter = ctx.painter2D;

            DrawGraphLines(painter);
            DrawMcpFillLevelGraph(painter);
            DrawMcpEmptiedGraph(painter);
            CreateLegends(painter);
        }

        private void DrawGraphLines(Painter2D painter)
        {
            painter.lineJoin = LineJoin.Round;
            painter.lineCap = LineCap.Round;
            painter.lineWidth = 1;

            var startX = GraphPadding.Left;
            var endX = resolvedStyle.width - GraphPadding.Right;
            var graphHeight = resolvedStyle.height - (LabelPadding.Top + LabelPadding.Bottom);
            _graphRect = new Rect(startX, LabelPadding.Top, endX - startX, graphHeight);
            for (var i = 0; i < VALUE_COUNT; i++)
            {
                painter.strokeColor = Color.gray;
                painter.BeginPath();
                painter.MoveTo(new Vector2(startX, graphHeight / (VALUE_COUNT - 1) * i + LabelPadding.Top));
                painter.LineTo(new Vector2(endX, graphHeight / (VALUE_COUNT - 1) * i + LabelPadding.Top));
                painter.Stroke();
            }
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
            // painter.lineWidth = 3;
            // painter.fillColor = _graphColor;
            //
            // painter.BeginPath();
            // painter.lineWidth = 3;
            //
            // for (var i = 0; i < _statGroups.Count; i++)
            // {
            //     if (_isSmooth)
            //     {
            //         // Work this out at some stage
            //         painter.LineTo(_statGroups[(i + 1) % _statGroups.Count].Point);
            //     }
            //     else
            //     {
            //         painter.LineTo(_statGroups[(i + 1) % _statGroups.Count].Point);
            //     }
            // }
            //
            // if (!_isSmooth) painter.ClosePath();
            // painter.Fill();
        }

        private void DrawMcpEmptiedGraph(Painter2D painter)
        {
            for (int i = 0; i < _mcpEmptiedTimestamps.Count; i++)
            {
            }
        }

        private void CreateLegends(Painter2D painter)
        {
        }

        private Vector2 GetHourlyMcpEmptiedPoint(int count, int minCount, int maxCount, DateTime hour)
        {
            var startX = LabelPadding.Left;
            var endX = resolvedStyle.width - LabelPadding.Right;
            var graphHeight = resolvedStyle.height - (LabelPadding.Top + LabelPadding.Bottom);
            var graphWidth = resolvedStyle.width - (LabelPadding.Left + LabelPadding.Right);
            var x = (float)(graphWidth / HOUR_COUNT * (hour.Hour + hour.Minute / 60f)) + startX;
            var y = (float)(graphHeight / (maxCount - minCount) * (count - minCount)) + LabelPadding.Top;
            return new Vector2(x, y);
        }
    }
}