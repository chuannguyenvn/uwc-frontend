using System;
using System.Collections.Generic;
using System.Linq;
using Authentication;
using Commons.Communications.Settings;
using Commons.Endpoints;
using Requests;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class ChoiceSettingListEntry : SettingListEntry
    {
        private readonly Func<string> _initializingCallback;
        private readonly bool _isSettingsView;
        private VisualElement _optionsContainer;
        private Dictionary<string, Option> _optionsByName = new();

        public ChoiceSettingListEntry(string name, Func<string> initializingCallback, List<string> optionDisplayNames,
            Dictionary<string, Action> optionCallbacks,
            bool isSettingsView = true) : base(name)
        {
            _initializingCallback = initializingCallback;
            _isSettingsView = isSettingsView;

            ConfigureUss(nameof(ChoiceSettingListEntry));

            CreateOptionsContainer();
            CreateOptions(optionDisplayNames, optionCallbacks);
            ModifySettingNameText();

            DataStoreManager.Setting.Settings.DataUpdated += _ => Refresh();
        }

        public void Refresh()
        {
            OptionSelectedHandler(_initializingCallback?.Invoke());
        }

        private void CreateOptionsContainer()
        {
            _optionsContainer = new VisualElement { name = "OptionsContainer" };
            Add(_optionsContainer);
        }

        private void CreateOptions(List<string> settingNames, Dictionary<string, Action> options)
        {
            for (int i = 0; i < settingNames.Count; i++)
            {
                var settingName = settingNames[i];
                var settingValue = options.Keys.ElementAt(i);
                var settingCallback = options.Values.ElementAt(i);

                var newOption = new Option(settingName, settingValue, () =>
                {
                    OptionSelectedHandler(settingValue);
                    settingCallback?.Invoke();

                    if (!_isSettingsView) return;

                    var newSetting = GetFirstAncestorOfType<SettingsView>().Setting;
                    DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest(
                        Endpoints.Setting.UpdateSetting,
                        new UpdateSettingRequest()
                        {
                            AccountId = AuthenticationManager.Instance.UserAccountId,
                            NewSetting = newSetting,
                        }));
                });
                _optionsContainer.Add(newOption);
                _optionsByName.Add(settingValue, newOption);
            }

            _optionsByName.Values.First().Activate();
        }

        private void ModifySettingNameText()
        {
            SettingNameText.AddToClassList("black-text");
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