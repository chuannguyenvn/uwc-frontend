using System;
using System.Collections.Generic;
using System.Linq;
using Commons.Models;
using LocalizationNS;
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

            Title.text = Localization.GetSentence(Sentence.McpsView.MAJOR_COLLECTION_POINT);

            CreateDetails();
            CreateGraph();
        }

        private void CreateDetails()
        {
            _addressEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.McpsView.ADDRESS));
            AddContent(_addressEntry);

            _latitudeEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.McpsView.LATITUDE));
            AddContent(_latitudeEntry);

            _longitudeEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.McpsView.LONGITUDE));
            AddContent(_longitudeEntry);

            _fillLevelEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.McpsView.FILL_LEVEL));
            AddContent(_fillLevelEntry);

            _lastEmptiedEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.McpsView.LAST_EMPTIED));
            AddContent(_lastEmptiedEntry);
        }

        private void CreateGraph()
        {
            _fillLevelGraph = new Graph(false, true, false);
            AddContent(_fillLevelGraph);

            _fillLevelGraph.ConfigureAreaGraph(Localization.GetSentence(Sentence.McpsView.FILL_LEVEL_BY_HOUR), new(121f / 255, 225f / 255, 153f / 255, 1), true);
        }

        public override void SetContent(McpData data)
        {
            _addressEntry.SetValue(data.Address);
            _latitudeEntry.SetValue(data.Latitude.ToString());
            _longitudeEntry.SetValue(data.Longitude.ToString());
            _fillLevelEntry.SetValue((DataStoreManager.Mcps.FillLevel.Data.FillLevelsById[data.Id] * 100).ToString("F1") + " %");
            _lastEmptiedEntry.SetValue(data.McpEmptyRecords.Count > 0
                ? data.McpEmptyRecords.Last().Timestamp.ToString("hh:mmtt dd/MM/yy")
                : Localization.GetSentence(Sentence.McpsView.NEVER));

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