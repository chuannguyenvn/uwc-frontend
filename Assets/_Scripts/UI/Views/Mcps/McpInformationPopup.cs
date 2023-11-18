using System;
using System.Collections.Generic;
using Commons.Models;
using UI.Base;
using UI.Reusables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Views.Mcps
{
    public class McpInformationPopup : DataBasedFullscreenPopup<McpData>
    {
        private Graph _fillLevelGraph;

        public McpInformationPopup()
        {
            ConfigureUss(nameof(McpInformationPopup));
        }

        public override void SetContent(McpData data)
        {
            _fillLevelGraph = new Graph();
            AddContent(_fillLevelGraph);

            var randomDateTimes = new List<DateTime>();
            var randomValues = new List<float>();
            for (int i = 47; i >= 0; i--)
            {
                randomDateTimes.Add(DateTime.Now.AddMinutes(-30 * i));
                randomValues.Add(Random.Range(0f, 1f));
            }

            _fillLevelGraph.AddLineGraph("Mcp fill level", Color.red, randomDateTimes, randomValues, true);
        }
    }
}