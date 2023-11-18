using System;
using System.Collections.Generic;
using System.Linq;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Reusables
{
    public class Graph : AdaptiveElement
    {
        private List<TextElement> _areaGraphLabels;
        private List<TextElement> _lineGraphLabels;
        private List<TextElement> _timestamps;

        private bool _isGraphTimestampRelative = true;

        // Line graph
        private bool _useLineGraph = false;
        private string _lineGraphName;
        private Color _lineGraphColor;
        private List<DateTime> _lineGraphTimestamps;
        private List<float> _lineGraphValues;
        private bool _isLineGraphPercentage;

        // Area graph
        private bool _useAreaGraph = false;
        private string _areaGraphName;
        private Color _areaGraphColor;
        private List<DateTime> _areaGraphTimestamps;
        private List<float> _areaGraphValues;
        private bool _isAreaGraphPercentage;

        private VisualElement _legendsContainer;

        private const int AXES_COUNT = 9;
        private const int TIMESTAMP_COUNT = 24;
        private static readonly Padding GraphPadding = new(96, 96, 64, 128);
        private static readonly Padding LabelPadding = new(64, 64, 64, 128);
        private static readonly Color GraphLineColor = new(217f / 255, 217f / 255, 217f / 255, 1);
        private Rect _graphRect;

        public Graph() : base(nameof(Graph))
        {
            ConfigureUss(nameof(Graph));

            CreateLabels();

            _legendsContainer = new VisualElement { name = "LegendContainer" };
            Add(_legendsContainer);

            generateVisualContent += GenerateVisualContent;
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        public void AddLineGraph(string name, Color color, bool isPercentage)
        {
            _useLineGraph = true;
            _lineGraphName = name;
            _lineGraphColor = color;
            _isLineGraphPercentage = isPercentage;
            CreateLegend(name, color);
        }

        public void AddAreaGraph(string name, Color color, bool isPercentage)
        {
            _useAreaGraph = true;
            _areaGraphName = name;
            _areaGraphColor = color;
            _isAreaGraphPercentage = isPercentage;
            CreateLegend(name, color);
        }

        public void UpdateLineGraph(List<DateTime> timestamps, List<float> values)
        {
            if (!_useLineGraph) throw new Exception("Line graph not enabled");
            _lineGraphTimestamps = timestamps;
            _lineGraphValues = values;
        }

        public void UpdateAreaGraph(List<DateTime> timestamps, List<float> values)
        {
            if (!_useAreaGraph) throw new Exception("Area graph not enabled");
            _areaGraphTimestamps = timestamps;
            _areaGraphValues = values;
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            ModifyAxeValues();
            ModifyTimestamps();
        }

        private void CreateLabels()
        {
            _areaGraphLabels = new List<TextElement>();
            _lineGraphLabels = new List<TextElement>();
            _timestamps = new List<TextElement>();

            for (var i = 0; i < AXES_COUNT; i++)
            {
                var lineLabel = new TextElement
                {
                    name = "LineLabel"
                };
                _lineGraphLabels.Add(lineLabel);
                lineLabel.AddToClassList("grey-text");
                lineLabel.AddToClassList("graph-text");
                Add(lineLabel);

                var areaLabel = new TextElement
                {
                    name = "AreaLabel"
                };
                _areaGraphLabels.Add(areaLabel);
                areaLabel.AddToClassList("grey-text");
                areaLabel.AddToClassList("graph-text");
                Add(areaLabel);
            }

            for (var i = 0; i < TIMESTAMP_COUNT; i++)
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

        private void CreateLegend(string name, Color color)
        {
            var legendIcon = new VisualElement { name = "LegendIcon" };
            legendIcon.AddToClassList("legend-icon");
            legendIcon.style.backgroundColor = color;
            _legendsContainer.Add(legendIcon);

            var legendText = new TextElement { name = "LegendTitle", text = name };
            legendText.AddToClassList("sub-text");
            legendText.AddToClassList("grey-text");
            _legendsContainer.Add(legendText);
        }

        private void GenerateVisualContent(MeshGenerationContext ctx)
        {
            var painter = ctx.painter2D;

            DrawAxes(painter);
            if (_useLineGraph) DrawLineGraph(painter);
            if (_useAreaGraph) DrawAreaGraph(painter);
        }

        private void DrawAxes(Painter2D painter)
        {
            painter.lineWidth = 1;
            painter.strokeColor = GraphLineColor;

            var startX = GraphPadding.Left;
            var endX = resolvedStyle.width - GraphPadding.Right;
            var graphHeight = resolvedStyle.height - (LabelPadding.Top + LabelPadding.Bottom);
            for (var i = 0; i < AXES_COUNT - 1; i++)
            {
                painter.BeginPath();
                painter.MoveTo(new Vector2(startX, graphHeight / (AXES_COUNT - 1) * i + LabelPadding.Top));
                painter.LineTo(new Vector2(endX, graphHeight / (AXES_COUNT - 1) * i + LabelPadding.Top));
                painter.Stroke();
            }

            _graphRect = new Rect(startX, LabelPadding.Top, endX - startX, graphHeight);
        }

        private void ModifyAxeValues()
        {
            var startX = LabelPadding.Left;
            var endX = resolvedStyle.width - LabelPadding.Right;
            var graphHeight = resolvedStyle.height - (LabelPadding.Top + LabelPadding.Bottom);
            var size = new Vector2(50, 20);

            for (var i = 0; i < AXES_COUNT; i++)
            {
                if (_useLineGraph)
                {
                    var lineGraphLabel = _lineGraphLabels[i];
                    var lineGraphLabelXPosition = _useAreaGraph ? endX : startX;
                    var lineGraphLabelPosition = new Vector2(lineGraphLabelXPosition, graphHeight / (AXES_COUNT - 1) * i + LabelPadding.Top);
                    lineGraphLabel.style.left = lineGraphLabelPosition.x - size.x / 2;
                    lineGraphLabel.style.right = resolvedStyle.width - (lineGraphLabelPosition.x + size.x / 2);
                    lineGraphLabel.style.top = lineGraphLabelPosition.y - size.y / 2;
                    lineGraphLabel.style.bottom = resolvedStyle.height - (lineGraphLabelPosition.y + size.y / 2);

                    var labelText = _isLineGraphPercentage ? $"{100 - (AXES_COUNT - 1 - i) * 100 / (AXES_COUNT - 1)}%" : $"{AXES_COUNT - 1 - i}";
                    lineGraphLabel.text = labelText;
                }

                if (_useAreaGraph)
                {
                    var areaGraphLabel = _areaGraphLabels[i];
                    var areaGraphLabelPosition = new Vector2(startX, graphHeight / (AXES_COUNT - 1) * i + LabelPadding.Top);
                    areaGraphLabel.style.left = areaGraphLabelPosition.x - size.x / 2;
                    areaGraphLabel.style.right = resolvedStyle.width - (areaGraphLabelPosition.x + size.x / 2);
                    areaGraphLabel.style.top = areaGraphLabelPosition.y - size.y / 2;
                    areaGraphLabel.style.bottom = resolvedStyle.height - (areaGraphLabelPosition.y + size.y / 2);

                    var labelText = _isAreaGraphPercentage ? $"{(AXES_COUNT - 1 - i) * 100 / (AXES_COUNT - 1)}%" : $"{AXES_COUNT - 1 - i}";
                    areaGraphLabel.text = labelText;
                }
            }
        }

        private void ModifyTimestamps()
        {
            var startX = GraphPadding.Left;
            var y = resolvedStyle.height - GraphPadding.Bottom;
            var graphWidth = resolvedStyle.width - (GraphPadding.Left + GraphPadding.Right);
            var size = new Vector2(50, 20);

            for (var i = 0; i < TIMESTAMP_COUNT; i++)
            {
                var timestamp = _timestamps[i];

                var position = new Vector2(graphWidth / (TIMESTAMP_COUNT - 1) * i + startX, y + 20);
                timestamp.style.left = position.x - size.x / 2;
                timestamp.style.right = resolvedStyle.width - (position.x + size.x / 2);
                timestamp.style.top = position.y - size.y / 2;
                timestamp.style.bottom = resolvedStyle.height - (position.y + size.y / 2);

                if (_isGraphTimestampRelative) timestamp.text = $"{i}:00";
                else timestamp.text = DateTime.Now.AddHours(-i).ToString("HH:mm");
            }
        }

        private void DrawLineGraph(Painter2D painter)
        {
            var minValue = 0;
            var maxValue = _isLineGraphPercentage ? 1 : _lineGraphValues.Max() * 1.1f;

            painter.lineCap = LineCap.Round;
            painter.lineJoin = LineJoin.Bevel;
            painter.lineWidth = 3;
            painter.strokeColor = _lineGraphColor;

            painter.BeginPath();
            painter.MoveTo(_graphRect.position + new Vector2(0, _graphRect.height));
            for (int i = 0; i < _lineGraphValues.Count; i++)
            {
                if (_lineGraphTimestamps[i] < DateTime.Now.AddHours(-24)) continue;

                var point = GetGraphPoint(
                    _lineGraphValues[i],
                    minValue,
                    maxValue,
                    _lineGraphTimestamps[i],
                    DateTime.Now.AddHours(-24),
                    DateTime.Now);
                painter.LineTo(point);
            }

            painter.Stroke();
        }

        private void DrawAreaGraph(Painter2D painter)
        {
            var minValue = 0;
            var maxValue = _isLineGraphPercentage ? 1 : _lineGraphValues.Max() * 1.1f;

            painter.fillColor = _areaGraphColor;

            painter.BeginPath();
            painter.MoveTo(_graphRect.position + new Vector2(0, _graphRect.height));
            for (int i = 0; i < _areaGraphTimestamps.Count; i++)
            {
                if (_areaGraphTimestamps[i] < DateTime.Now.AddHours(-24)) continue;

                var point = GetGraphPoint(
                    _areaGraphValues[i],
                    minValue,
                    maxValue,
                    _areaGraphTimestamps[i],
                    DateTime.Now.AddHours(-24),
                    DateTime.Now);
                painter.LineTo(point);
            }

            painter.LineTo(_graphRect.position + new Vector2(_graphRect.width, _graphRect.height));
            painter.Fill();
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