using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI
{
    internal class Shadow : VisualElement
    {
        private Vertex[] k_Vertices;

        public Shadow()
        {
            generateVisualContent += OnGenerateVisualContent;
        }

        public int shadowCornerRadius { get; set; }
        public float shadowScale { get; set; }
        public int shadowOffsetX { get; set; }
        public int shadowOffsetY { get; set; }
        public int shadowCornerSubdivisions => 3;

        private void OnGenerateVisualContent(MeshGenerationContext ctx)
        {
            var r = contentRect;

            float left = 0;
            var right = r.width;
            float top = 0;
            var bottom = r.height;
            var halfSpread = shadowCornerRadius / 2f;
            var curveSubdivisions = shadowCornerSubdivisions;
            var totalVertices = 12 + (curveSubdivisions - 1) * 4;

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


            // Outside edge
            k_Vertices = new Vertex[totalVertices];

            k_Vertices[0].position = new Vector3(left + halfSpread, bottom + halfSpread, Vertex.nearZ);
            k_Vertices[1].position = new Vector3(left + halfSpread, top - halfSpread, Vertex.nearZ);
            k_Vertices[2].position = new Vector3(right - halfSpread, top - halfSpread, Vertex.nearZ);
            k_Vertices[3].position = new Vector3(right - halfSpread, bottom + halfSpread, Vertex.nearZ);
            k_Vertices[0].tint = Color.clear;
            k_Vertices[1].tint = Color.clear;
            k_Vertices[2].tint = Color.clear;
            k_Vertices[3].tint = Color.clear;

            k_Vertices[8].position = new Vector3(right + halfSpread, bottom - halfSpread, Vertex.nearZ);
            k_Vertices[9].position = new Vector3(left - halfSpread, bottom - halfSpread, Vertex.nearZ);
            k_Vertices[10].position = new Vector3(left - halfSpread, top + halfSpread, Vertex.nearZ);
            k_Vertices[11].position = new Vector3(right + halfSpread, top + halfSpread, Vertex.nearZ);
            k_Vertices[8].tint = Color.clear;
            k_Vertices[9].tint = Color.clear;
            k_Vertices[10].tint = Color.clear;
            k_Vertices[11].tint = Color.clear;

            // Inside rectangle
            k_Vertices[4].position = new Vector3(0 + halfSpread, r.height - halfSpread, Vertex.nearZ);
            k_Vertices[5].position = new Vector3(0 + halfSpread, 0 + halfSpread, Vertex.nearZ);
            k_Vertices[6].position = new Vector3(r.width - halfSpread, 0 + halfSpread, Vertex.nearZ);
            k_Vertices[7].position = new Vector3(r.width - halfSpread, r.height - halfSpread, Vertex.nearZ);
            k_Vertices[4].tint = resolvedStyle.color;
            k_Vertices[5].tint = resolvedStyle.color;
            k_Vertices[6].tint = resolvedStyle.color;
            k_Vertices[7].tint = resolvedStyle.color;

            // Top right corner
            for (var i = 0; i < curveSubdivisions - 1; i++)
            {
                var vertexId = 12 + i;
                var angle = Mathf.PI * 0.5f / curveSubdivisions + Mathf.PI * 0.5f / curveSubdivisions * i;

                k_Vertices[vertexId].position = new Vector3(r.width - halfSpread + Mathf.Sin(angle) * shadowCornerRadius,
                    0 + halfSpread + -Mathf.Cos(angle) * shadowCornerRadius, Vertex.nearZ);
                k_Vertices[vertexId].tint = Color.clear;
            }

            // Bottom right corner
            for (var i = 0; i < curveSubdivisions - 1; i++)
            {
                var vertexId = 12 + i + (curveSubdivisions - 1);
                var angle = Mathf.PI * 0.5f + Mathf.PI * 0.5f / curveSubdivisions + Mathf.PI * 0.5f / curveSubdivisions * i;

                k_Vertices[vertexId].position = new Vector3(r.width - halfSpread + Mathf.Sin(angle) * shadowCornerRadius,
                    r.height - halfSpread + -Mathf.Cos(angle) * shadowCornerRadius, Vertex.nearZ);
                k_Vertices[vertexId].tint = Color.clear;
            }

            // Bottom left corner
            for (var i = 0; i < curveSubdivisions - 1; i++)
            {
                var vertexId = 12 + i + (curveSubdivisions - 1) * 2;
                var angle = Mathf.PI + Mathf.PI * 0.5f / curveSubdivisions + Mathf.PI * 0.5f / curveSubdivisions * i;

                k_Vertices[vertexId].position = new Vector3(0 + halfSpread + Mathf.Sin(angle) * shadowCornerRadius,
                    r.height - halfSpread + -Mathf.Cos(angle) * shadowCornerRadius, Vertex.nearZ);
                k_Vertices[vertexId].tint = Color.clear;
            }

            // Top left corner
            for (var i = 0; i < curveSubdivisions - 1; i++)
            {
                var vertexId = 12 + i + (curveSubdivisions - 1) * 3;
                var angle = Mathf.PI * 1.5f + Mathf.PI * 0.5f / curveSubdivisions + Mathf.PI * 0.5f / curveSubdivisions * i;

                k_Vertices[vertexId].position = new Vector3(0 + halfSpread + Mathf.Sin(angle) * shadowCornerRadius,
                    0 + halfSpread + -Mathf.Cos(angle) * shadowCornerRadius, Vertex.nearZ);
                k_Vertices[vertexId].tint = Color.clear;
            }

            var dimensions = new Vector3(r.width, r.height, Vertex.nearZ);

            for (var i = 0; i < k_Vertices.Length; i++)
            {
                // Do not scale the inner rectangle
                var newPos = k_Vertices[i].position;
                newPos = newPos + new Vector3(shadowOffsetX, shadowOffsetY, 0);

                if (i >= 4 && i <= 7)
                {
                    // Do nothing
                }
                else
                {
                    newPos = (newPos - dimensions * 0.5f) * shadowScale + dimensions * 0.5f;
                }

                // Scale verticles using scale factor
                k_Vertices[i].position = newPos;
            }

            var tris = new List<ushort>();
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
                6, 7, 4
            });

            for (ushort i = 0; i < curveSubdivisions; i++)
                if (i == 0)
                    tris.AddRange(new ushort[] { 2, 12, 6 });
                else if (i == curveSubdivisions - 1)
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1), 11, 6 });
                else
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1), (ushort)(12 + i), 6 });

            for (ushort i = 0; i < curveSubdivisions; i++)
                if (i == 0)
                    tris.AddRange(new ushort[] { 7, 8, 14 });
                else if (i == curveSubdivisions - 1)
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1 + (curveSubdivisions - 1)), 3, 7 });
                else
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1 + (curveSubdivisions - 1)), (ushort)(12 + i + (curveSubdivisions - 1)), 7 });

            for (ushort i = 0; i < curveSubdivisions; i++)
                if (i == 0)
                    tris.AddRange(new ushort[] { 4, 0, 16 });
                else if (i == curveSubdivisions - 1)
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1 + 2 * (curveSubdivisions - 1)), 9, 4 });
                else
                    tris.AddRange(new ushort[]
                        { (ushort)(12 + i - 1 + 2 * (curveSubdivisions - 1)), (ushort)(12 + i + 2 * (curveSubdivisions - 1)), 4 });

            for (ushort i = 0; i < curveSubdivisions; i++)
                if (i == 0)
                    tris.AddRange(new ushort[] { 5, 10, 18 });
                else if (i == curveSubdivisions - 1)
                    tris.AddRange(new ushort[] { (ushort)(12 + i - 1 + 3 * (curveSubdivisions - 1)), 1, 5 });
                else
                    tris.AddRange(new ushort[]
                        { (ushort)(12 + i - 1 + 3 * (curveSubdivisions - 1)), (ushort)(12 + i + 3 * (curveSubdivisions - 1)), 5 });

            var mwd = ctx.Allocate(k_Vertices.Length, tris.Count);
            mwd.SetAllVertices(k_Vertices);
            mwd.SetAllIndices(tris.ToArray());
        }

        [Preserve]
        public new class UxmlFactory : UxmlFactory<Shadow, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            // Offsets. Tweak to have e.g. a shadow below and to the right of an element.
            private readonly UxmlIntAttributeDescription offsetXAttr = new() { name = "shadow-offset-x", defaultValue = 0 };

            private readonly UxmlIntAttributeDescription offsetYAttr = new() { name = "shadow-offset-y", defaultValue = 0 };

            // Rounded corner radius. Increase to make the shadow "fluffier"
            private readonly UxmlIntAttributeDescription radiusAttr = new() { name = "shadow-corner-radius", defaultValue = 10 };

            // Scale. Increase to make the shadow extend farther away from the element.
            private readonly UxmlFloatAttributeDescription scaleAttr = new() { name = "shadow-scale", defaultValue = 1.1f };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            // Buggy right now - always set to 3.
            /*UxmlIntAttributeDescription subdivisionsAttr =
                new UxmlIntAttributeDescription { name="shadow-corner-subdivisions", defaultValue = 3};*/

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var shadow = ve as Shadow;

                shadow.shadowCornerRadius = radiusAttr.GetValueFromBag(bag, cc);
                shadow.shadowScale = scaleAttr.GetValueFromBag(bag, cc);
                shadow.shadowOffsetX = offsetXAttr.GetValueFromBag(bag, cc);
                shadow.shadowOffsetY = offsetYAttr.GetValueFromBag(bag, cc);
                //shadow.shadowCornerSubdivisions = subdivisionsAttr.GetValueFromBag(bag, cc);
            }
        }
    }
}