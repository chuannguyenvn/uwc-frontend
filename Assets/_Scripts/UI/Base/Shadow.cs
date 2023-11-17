using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class Shadow : VisualElement
    {
        private VisualElement _element;

        private Vertex[] _kVertices;
        private Color _clearColor;

        private readonly Color _shadowColor;
        private int _cornerRadius;
        private float _scale;
        private int _offsetX;
        private int _offsetY;
        private int _cornerSubdivisions = 3;


        public Shadow(VisualElement element, int cornerRadius = -1, float scale = 1, int offsetX = 0, int offsetY = 0) : this(element, Color.black,
            cornerRadius, scale, offsetX, offsetY)
        {
        }

        public Shadow(VisualElement element, Color shadowColor, int cornerRadius = -1, float scale = 1, int offsetX = 0, int offsetY = 0)
        {
            _element = element;
            _shadowColor = shadowColor;
            _cornerRadius = cornerRadius;
            _scale = scale;
            _offsetX = offsetX;
            _offsetY = offsetY;

            Add(element);

            generateVisualContent += OnGenerateVisualContent;
        }

        private void OnGenerateVisualContent(MeshGenerationContext ctx)
        {
            Rect r = contentRect;

            if (_cornerRadius == -1) _cornerRadius = (int)_element.resolvedStyle.borderBottomLeftRadius;

            float left = 0;
            float right = r.width;
            float top = 0;
            float bottom = r.height;
            float halfSpread = _cornerRadius / 2f;
            int curveSubdivisions = _cornerSubdivisions;
            int totalVertices = 12 + (curveSubdivisions - 1) * 4;

            /*
        4/5/6/7 = inset rectangle (rect-shadowInsetAmount)
        0/1/2/3/8/9/10/11 = outset rectangle (rect+shadowSpread)
            1        2     12 => 12+(subdivisions-1)
           \|         /
       10 - 5========6 - 11
            |        |
            |        |
            |        |
            |        |
        9 - 4========7 - 8
           /          \
            0        3     (12+subdivisions-1)+1 => 12 + 2*(subdivisions-1) + 1
        */

            //Get colour to prevent darkening when blending to transparent
            _clearColor = new(_shadowColor.r, _shadowColor.g, _shadowColor.b, 0);

            // Outside edge
            _kVertices = new Vertex[totalVertices];

            _kVertices[0].position = new Vector3(left + halfSpread, bottom + halfSpread, Vertex.nearZ);
            _kVertices[1].position = new Vector3(left + halfSpread, top - halfSpread, Vertex.nearZ);
            _kVertices[2].position = new Vector3(right - halfSpread, top - halfSpread, Vertex.nearZ);
            _kVertices[3].position = new Vector3(right - halfSpread, bottom + halfSpread, Vertex.nearZ);
            _kVertices[0].tint = _clearColor;
            _kVertices[1].tint = _clearColor;
            _kVertices[2].tint = _clearColor;
            _kVertices[3].tint = _clearColor;

            _kVertices[8].position = new Vector3(right + halfSpread, bottom - halfSpread, Vertex.nearZ);
            _kVertices[9].position = new Vector3(left - halfSpread, bottom - halfSpread, Vertex.nearZ);
            _kVertices[10].position = new Vector3(left - halfSpread, top + halfSpread, Vertex.nearZ);
            _kVertices[11].position = new Vector3(right + halfSpread, top + halfSpread, Vertex.nearZ);
            _kVertices[8].tint = _clearColor;
            _kVertices[9].tint = _clearColor;
            _kVertices[10].tint = _clearColor;
            _kVertices[11].tint = _clearColor;

            // Inside rectangle
            _kVertices[4].position = new Vector3(0 + halfSpread, r.height - halfSpread, Vertex.nearZ);
            _kVertices[5].position = new Vector3(0 + halfSpread, 0 + halfSpread, Vertex.nearZ);
            _kVertices[6].position = new Vector3(r.width - halfSpread, 0 + halfSpread, Vertex.nearZ);
            _kVertices[7].position = new Vector3(r.width - halfSpread, r.height - halfSpread, Vertex.nearZ);
            _kVertices[4].tint = _shadowColor;
            _kVertices[5].tint = _shadowColor;
            _kVertices[6].tint = _shadowColor;
            _kVertices[7].tint = _shadowColor;

            // Top right corner
            for (int i = 0; i < curveSubdivisions - 1; i++)
            {
                int vertexId = 12 + i;
                float angle = Mathf.PI * 0.5f / curveSubdivisions + Mathf.PI * 0.5f / curveSubdivisions * i;

                _kVertices[vertexId].position = new Vector3(r.width - halfSpread + Mathf.Sin(angle) * _cornerRadius,
                    0 + halfSpread + -Mathf.Cos(angle) * _cornerRadius, Vertex.nearZ);
                _kVertices[vertexId].tint = _clearColor;
            }

            // Bottom right corner
            for (int i = 0; i < curveSubdivisions - 1; i++)
            {
                int vertexId = 12 + i + (curveSubdivisions - 1);
                float angle = Mathf.PI * 0.5f + Mathf.PI * 0.5f / curveSubdivisions + Mathf.PI * 0.5f / curveSubdivisions * i;

                _kVertices[vertexId].position = new Vector3(r.width - halfSpread + Mathf.Sin(angle) * _cornerRadius,
                    r.height - halfSpread + -Mathf.Cos(angle) * _cornerRadius, Vertex.nearZ);
                _kVertices[vertexId].tint = _clearColor;
            }

            // Bottom left corner
            for (int i = 0; i < curveSubdivisions - 1; i++)
            {
                int vertexId = 12 + i + (curveSubdivisions - 1) * 2;
                float angle = Mathf.PI + Mathf.PI * 0.5f / curveSubdivisions + Mathf.PI * 0.5f / curveSubdivisions * i;

                _kVertices[vertexId].position = new Vector3(0 + halfSpread + Mathf.Sin(angle) * _cornerRadius,
                    r.height - halfSpread + -Mathf.Cos(angle) * _cornerRadius, Vertex.nearZ);
                _kVertices[vertexId].tint = _clearColor;
            }

            // Top left corner
            for (int i = 0; i < curveSubdivisions - 1; i++)
            {
                int vertexId = 12 + i + (curveSubdivisions - 1) * 3;
                float angle = Mathf.PI * 1.5f + Mathf.PI * 0.5f / curveSubdivisions + Mathf.PI * 0.5f / curveSubdivisions * i;

                _kVertices[vertexId].position = new Vector3(0 + halfSpread + Mathf.Sin(angle) * _cornerRadius,
                    0 + halfSpread + -Mathf.Cos(angle) * _cornerRadius, Vertex.nearZ);
                _kVertices[vertexId].tint = _clearColor;
            }

            Vector3 dimensions = new Vector3(r.width, r.height, Vertex.nearZ);

            for (int i = 0; i < _kVertices.Length; i++)
            {
                // Do not scale the inner rectangle
                Vector3 newPos = _kVertices[i].position;
                newPos = newPos + new Vector3(_offsetX, _offsetY, 0);

                if (i >= 4 && i <= 7)
                {
                    // Do nothing
                }
                else
                {
                    newPos = (newPos - dimensions * 0.5f) * _scale + dimensions * 0.5f;
                }

                // Scale verticles using scale factor
                _kVertices[i].position = newPos;
            }

            List<ushort> tris = new List<ushort>();
            tris.AddRange(new ushort[]
            {
                1, 6, 5,
                2, 6, 1,
                6, 11, 8,
                6, 8, 7,
                4, 7, 3,
                4, 3, 0,
                10, 5, 4,
                10, 4, 9,
                5, 6, 4,
                6, 7, 4,
            });

            for (ushort i = 0; i < curveSubdivisions; i++)
            {
                if (i == 0)
                {
                    tris.AddRange(new ushort[] { 2, 12, 6 });
                }
                else if (i == curveSubdivisions - 1)
                {
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1), 11, 6 });
                }
                else
                {
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1), (ushort)(12 + i), 6 });
                }
            }

            for (ushort i = 0; i < curveSubdivisions; i++)
            {
                if (i == 0)
                {
                    tris.AddRange(new ushort[] { 7, 8, 14 });
                }
                else if (i == curveSubdivisions - 1)
                {
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1 + (curveSubdivisions - 1)), 3, 7 });
                }
                else
                {
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1 + (curveSubdivisions - 1)), (ushort)(12 + i + (curveSubdivisions - 1)), 7 });
                }
            }

            for (ushort i = 0; i < curveSubdivisions; i++)
            {
                if (i == 0)
                {
                    tris.AddRange(new ushort[] { 4, 0, 16 });
                }
                else if (i == curveSubdivisions - 1)
                {
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1 + 2 * (curveSubdivisions - 1)), 9, 4 });
                }
                else
                {
                    tris.AddRange(new ushort[]
                        { (ushort)(12 + i - 1 + 2 * (curveSubdivisions - 1)), (ushort)(12 + i + 2 * (curveSubdivisions - 1)), 4 });
                }
            }

            for (ushort i = 0; i < curveSubdivisions; i++)
            {
                if (i == 0)
                {
                    tris.AddRange(new ushort[] { 5, 10, 18 });
                }
                else if (i == curveSubdivisions - 1)
                {
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1 + 3 * (curveSubdivisions - 1)), 1, 5 });
                }
                else
                {
                    tris.AddRange(new ushort[]
                        { (ushort)(12 + i - 1 + 3 * (curveSubdivisions - 1)), (ushort)(12 + i + 3 * (curveSubdivisions - 1)), 5 });
                }
            }

            MeshWriteData mwd = ctx.Allocate(_kVertices.Length, tris.Count);
            mwd.SetAllVertices(_kVertices);
            mwd.SetAllIndices(tris.ToArray());
        }
    }
}