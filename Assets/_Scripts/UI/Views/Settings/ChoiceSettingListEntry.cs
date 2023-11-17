using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class ChoiceSettingListEntry : SettingListEntry
    {
        private VisualElement _optionsContainer;
        private Dictionary<string, Option> _optionsByName = new();

        public ChoiceSettingListEntry(string name, Dictionary<string, Action> options) : base(name)
        {
            ConfigureUss(nameof(ChoiceSettingListEntry));

            CreateOptionsContainer();
            CreateOptions(options);
            ModifySettingNameText();
        }

        private void CreateOptionsContainer()
        {
            _optionsContainer = new VisualElement { name = "OptionsContainer" };
            Add(_optionsContainer);
        }

        private void CreateOptions(Dictionary<string, Action> options)
        {
            foreach (var (settingName, settingCallback) in options)
            {
                var newOption = new Option(settingName, () =>
                {
                    OptionSelectedHandler(settingName);
                    settingCallback?.Invoke();
                });
                _optionsContainer.Add(newOption);
                _optionsByName.Add(settingName, newOption);
            }

            _optionsByName.Values.First().Activate();
        }

        private void ModifySettingNameText()
        {
            SettingNameText.AddToClassList("black-text");
            SettingNameText.text += ":";
        }

        private void OptionSelectedHandler(string optionName)
        {
            foreach (var option in _optionsByName)
            {
                if (option.Key == optionName) option.Value.Activate();
                else option.Value.Deactivate();
            }
        }
    }
}