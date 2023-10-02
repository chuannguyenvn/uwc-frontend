using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class TriggerSettingListEntry : SettingListEntry
    {
        public TriggerSettingListEntry(string name, Action callback, bool isDangerous = false) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Settings/TriggerSettingListEntry"));
            AddToClassList("trigger-setting-list-entry");
            if (isDangerous) AddToClassList("dangerous");
        }
    }
}