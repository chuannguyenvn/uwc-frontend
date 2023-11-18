using System;
using System.Collections.Generic;
using System.Linq;
using Commons.Models;
using Requests;
using UI.Base;
using UI.Reusables;
using UnityEngine;

namespace UI.Views.Mcps
{
    public class McpInformationPopup : DataBasedFullscreenPopup<McpData>
    {
        private PopupInformationEntry _addressEntry;
        private PopupInformationEntry _latitudeEntry;
        private PopupInformationEntry _longitudeEntry;
        private PopupInformationEntry _fillLevelEntry;
        private PopupInformationEntry _lastEmptiedEntry;

        private Graph _fillLevelGraph;

        public McpInformationPopup()
        {
            ConfigureUss(nameof(McpInformationPopup));

            Title.text = "Major collection point";

            CreateDetails();
            CreateGraph();
        }

        private void CreateDetails()
        {
            _addressEntry = new PopupInformationEntry("Address");
            AddContent(_addressEntry);

            _latitudeEntry = new PopupInformationEntry("Latitude");
            AddContent(_latitudeEntry);

            _longitudeEntry = new PopupInformationEntry("Longitude");
            AddContent(_longitudeEntry);

            _fillLevelEntry = new PopupInformationEntry("Fill level");
            AddContent(_fillLevelEntry);

            _lastEmptiedEntry = new PopupInformationEntry("Last emptied");
            AddContent(_lastEmptiedEntry);
        }

        private void CreateGraph()
        {
            _fillLevelGraph = new Graph(false, true, false);
            AddContent(_fillLevelGraph);

            _fillLevelGraph.ConfigureAreaGraph("Mcp fill level", Color.red, true);
        }

        public override void SetContent(McpData data)
        {
            _addressEntry.SetValue(data.Address);
            _latitudeEntry.SetValue(data.Latitude.ToString());
            _longitudeEntry.SetValue(data.Longitude.ToString());
            _fillLevelEntry.SetValue(DataStoreManager.Mcps.FillLevel.Data.FillLevelsById[data.Id].ToString() + " %");
            _lastEmptiedEntry.SetValue(data.McpEmptyRecords.Count > 0
                ? data.McpEmptyRecords.Last().Timestamp.ToString("hh:mm tt dd/MM")
                : "Never");

            var timestamps = new List<DateTime>();
            var values = new List<float>();
            data.McpFillLevelLogs = data.McpFillLevelLogs.OrderBy(log => log.Timestamp).ToList();
            foreach (var fillLevelLog in data.McpFillLevelLogs)
            {
                timestamps.Add(fillLevelLog.Timestamp);
                values.Add(fillLevelLog.McpFillLevel / 100f);
            }

            _fillLevelGraph.UpdateAreaGraph(timestamps, values);
        }
    }
}