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

        public GraphCard() : base(nameof(GraphCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/GraphCard"));
            GenerateMockData();
            CreateGraphValues();
            generateVisualContent += GenerateVisualContent;
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            ModifyGraphValues();
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

        private void CreateGraphValues()
        {
            _totalMcpFillLevelLabels = new List<TextElement>();
            _mcpEmptiedLabels = new List<TextElement>();


            for (var i = 0; i < 8; i++)
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
                mcpEmptiedLabel.style.position = Position.Absolute;
            }
        }

        private void GenerateVisualContent(MeshGenerationContext ctx)
        {
            var painter = ctx.painter2D;

            DrawGraphLines(painter);
            ModifyGraphValues();
            DrawMcpFillLevelGraph(painter);
            DrawMcpEmptiedGraph(painter);
            CreateTimestamps(painter);
            CreateLegends(painter);
        }

        private void DrawGraphLines(Painter2D painter)
        {
            painter.lineJoin = LineJoin.Round;
            painter.lineCap = LineCap.Round;
            painter.lineWidth = 1;

            var leftPadding = 96;
            var rightPadding = 96;
            var topPadding = 64;
            var bottomPadding = 128;
            var startX = leftPadding;
            var endX = resolvedStyle.width - rightPadding;
            var graphHeight = resolvedStyle.height - (topPadding + bottomPadding);
            for (var i = 0; i < 8; i++)
            {
                painter.strokeColor = Color.gray;
                painter.BeginPath();
                painter.MoveTo(new Vector2(startX, graphHeight / 7 * i + topPadding));
                painter.LineTo(new Vector2(endX, graphHeight / 7 * i + +topPadding));
                painter.Stroke();
            }
        }

        private void ModifyGraphValues()
        {
            var leftPadding = 64;
            var rightPadding = 64;
            var topPadding = 64;
            var bottomPadding = 128;
            var startX = leftPadding;
            var endX = resolvedStyle.width - rightPadding;
            var graphHeight = resolvedStyle.height - (topPadding + bottomPadding);

            for (var i = 0; i < 8; i++)
            {
                var size = new Vector2(50, 20);

                var mcpFillLevelLabelPosition = new Vector2(startX, graphHeight / 7 * i + topPadding);
                var mcpFillLevelLabel = _totalMcpFillLevelLabels[7 - i];
                mcpFillLevelLabel.style.left = mcpFillLevelLabelPosition.x - size.x / 2;
                mcpFillLevelLabel.style.right = resolvedStyle.width - (mcpFillLevelLabelPosition.x + size.x / 2);
                mcpFillLevelLabel.style.top = mcpFillLevelLabelPosition.y - size.y / 2;
                mcpFillLevelLabel.style.bottom = resolvedStyle.height - (mcpFillLevelLabelPosition.y + size.y / 2);

                var mcpEmptiedLabel = _mcpEmptiedLabels[7 - i];
                var mcpEmptiedLabelPosition = new Vector2(endX, graphHeight / 7 * i + topPadding);
                mcpEmptiedLabel.style.left = mcpEmptiedLabelPosition.x - size.x / 2;
                mcpEmptiedLabel.style.right = resolvedStyle.width - (mcpEmptiedLabelPosition.x + size.x / 2);
                mcpEmptiedLabel.style.top = mcpEmptiedLabelPosition.y - size.y / 2;
                mcpEmptiedLabel.style.bottom = resolvedStyle.height - (mcpEmptiedLabelPosition.y + size.y / 2);
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
        }

        private void CreateTimestamps(Painter2D painter)
        {
        }

        private void CreateLegends(Painter2D painter)
        {
        }
    }
}