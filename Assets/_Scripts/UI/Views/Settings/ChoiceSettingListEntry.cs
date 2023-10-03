using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class ChoiceSettingListEntry : SettingListEntry
    {
        public VisualElement OptionsContainer;
        public Dictionary<string, Option> OptionsByName = new();

        public ChoiceSettingListEntry(string name, Dictionary<string, Action> options) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Settings/ChoiceSettingListEntry"));
            AddToClassList("choice-setting-list-entry");

            OptionsContainer = new VisualElement { name = "OptionsContainer" };
            Add(OptionsContainer);

            foreach (var (settingName, settingCallback) in options)
            {
                var newOption = new Option(settingName, () =>
                {
                    OptionSelectedHandler(settingName);
                    settingCallback?.Invoke();
                });
                OptionsContainer.Add(newOption);
                OptionsByName.Add(settingName, newOption);
            }

            SettingNameText.text += ":";
            SettingNameText.AddToClassList("black-text");

            OptionsByName.Values.First().Activate();
        }

        public void OptionSelectedHandler(string optionName)
        {
            foreach (var option in OptionsByName)
            {
                if (option.Key == optionName) option.Value.Activate();
                else option.Value.Deactivate();
            }
        }
    }
}