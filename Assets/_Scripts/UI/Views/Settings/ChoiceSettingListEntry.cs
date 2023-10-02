using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class ChoiceSettingListEntry : SettingListEntry
    {
        public VisualElement OptionsContainer;

        public ChoiceSettingListEntry(string name, Dictionary<string, Action> options) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Settings/ChoiceSettingListEntry"));
            AddToClassList("choice-setting-list-entry");

            OptionsContainer = new VisualElement { name = "OptionsContainer" };
            Add(OptionsContainer);

            foreach (var (settingName, settingCallback) in options)
            {
                OptionsContainer.Add(new Option(settingName, settingCallback));
            }

            SettingNameText.text += ":";
            SettingNameText.AddToClassList("black-text");
        }
    }
}