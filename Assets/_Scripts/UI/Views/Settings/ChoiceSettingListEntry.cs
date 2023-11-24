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
        private VisualElement _optionsContainer;
        private Dictionary<string, Option> _optionsByName = new();

        public ChoiceSettingListEntry(string name, Func<string> initializingCallback, Dictionary<string, Action> optionCallbacks) : base(name)
        {
            _initializingCallback = initializingCallback;

            ConfigureUss(nameof(ChoiceSettingListEntry));

            CreateOptionsContainer();
            CreateOptions(optionCallbacks);
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

        private void CreateOptions(Dictionary<string, Action> options)
        {
            foreach (var (settingName, settingCallback) in options)
            {
                var newOption = new Option(settingName, () =>
                {
                    OptionSelectedHandler(settingName);
                    settingCallback?.Invoke();
                    
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
                _optionsByName.Add(settingName, newOption);
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