using System;
using System.Collections.Generic;
using Commons.Models;
using Commons.Types.SettingOptions;
using LocalizationNS;
using Requests;
using Settings;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class SettingsView : View
    {
        public Setting Setting;
        private SettingList _settingList;

        public SettingsView() : base(nameof(SettingsView))
        {
            ConfigureUss(nameof(SettingsView));

            AddToClassList(Configs.IS_DESKTOP ? "side-view" : "full-view");

            DataStoreManager.Setting.Settings.DataUpdated += data => Setting = data.Setting;

            CreateSettings();
            CreateNotificationSettings();
            CreateAccountSettings();

            _settingList.style.display = DisplayStyle.None;
        }

        private void CreateSettings()
        {
            _settingList = new SettingList();
            Add(_settingList);

            CreateInterfaceSettings();
        }

        private void CreateInterfaceSettings()
        {
            _settingList.Add(new SectionHeader(Localization.GetSentence(Sentence.SettingsView.INTERFACE_SETTINGS)));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.DARK_MODE), () => Setting.DarkMode.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.DarkMode = ToggleOption.On },
                { "Off", () => Setting.DarkMode = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.COLORBLIND_MODE), () => Setting.ColorblindMode.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.ColorblindMode = ToggleOption.On },
                { "Off", () => Setting.ColorblindMode = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.REDUCE_MOTION), () => Setting.ReducedMotionMode.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.ReducedMotionMode = ToggleOption.On },
                { "Off", () => Setting.ReducedMotionMode = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.LANGUAGE), () => Setting.Language.ToString(), new Dictionary<string, Action>
            {
                {
                    "English", () => { Setting.Language = Localization.LanguageOption = LanguageOption.English; }
                },
                {
                    "Vietnamese", () => { Setting.Language = Localization.LanguageOption = LanguageOption.Vietnamese; }
                },
            }));
        }

        private void CreateNotificationSettings()
        {
            _settingList.Add(new SectionHeader(Localization.GetSentence(Sentence.SettingsView.NOTIFICATION_SETTINGS)));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.MESSAGES), () => Setting.Messages.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.Messages = ToggleOption.On },
                { "Off", () => Setting.Messages = ToggleOption.Off },
            }));

            if (Configs.IS_DESKTOP)
            {
                _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.EMPLOYEES_LOGGED_IN), () => Setting.EmployeesLoggedIn.ToString(),
                    new Dictionary<string, Action>
                    {
                        { "On", () => Setting.EmployeesLoggedIn = ToggleOption.On },
                        { "Off", () => Setting.EmployeesLoggedIn = ToggleOption.Off },
                    }));

                _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.EMPLOYEES_LOGGED_OUT), () => Setting.EmployeesLoggedOut.ToString(),
                    new Dictionary<string, Action>
                    {
                        { "On", () => Setting.EmployeesLoggedOut = ToggleOption.On },
                        { "Off", () => Setting.EmployeesLoggedOut = ToggleOption.Off },
                    }));
            }

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.MCPS_ALMOST_FULL), () => Setting.McpsAlmostFull.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.McpsAlmostFull = ToggleOption.On },
                { "Off", () => Setting.McpsAlmostFull = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.MCPS_FULL), () => Setting.McpsFull.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.McpsFull = ToggleOption.On },
                { "Off", () => Setting.McpsFull = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.MCPS_EMPTIED), () => Setting.McpsEmptied.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.McpsEmptied = ToggleOption.On },
                { "Off", () => Setting.McpsEmptied = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.SOFTWARE_UPDATE_AVAILABLE), () => Setting.SoftwareUpdateAvailable.ToString(),
                new Dictionary<string, Action>
                {
                    { "On", () => Setting.SoftwareUpdateAvailable = ToggleOption.On },
                    { "Off", () => Setting.SoftwareUpdateAvailable = ToggleOption.Off },
                }));
        }

        private void CreateAccountSettings()
        {
            _settingList.Add(new SectionHeader(Localization.GetSentence(Sentence.SettingsView.ACCOUNT_SETTINGS)));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.ONLINE_STATUS), () => Setting.OnlineStatus.ToString(), new Dictionary<string, Action>
            {
                { "Online", () => { Setting.OnlineStatus = OnlineStatusOption.Online; } },
                { "Offline", () => { Setting.OnlineStatus = OnlineStatusOption.Offline; } },
            }));

            if (Configs.IS_DESKTOP)
            {
                _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.EXPORT_WORK_LOGS), () => { }));
                _settingList.Add(new TriggerSettingListEntry("Export work logs", () => { }));
            }

            _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.CHANGE_PERSONAL_INFORMATION), () => { }));

            _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.CHANGE_PASSWORD), () => { }));

            if (!Configs.IS_DESKTOP) _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.REGISTER_FACIAL_RECOGNITION), () => { }));

            _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.REPORT_PROBLEM), () => { }));

            _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.LOGOUT), () => { }, true));
        }

        public override void FocusView()
        {
            DataStoreManager.Setting.Settings.Focus();
            DataStoreManager.Setting.Settings.DataUpdated += _ =>
            {
                _settingList.style.display = DisplayStyle.Flex;
            };
        }

        public override void UnfocusView()
        {
            DataStoreManager.Setting.Settings.Unfocus();
            _settingList.style.display = DisplayStyle.None;
        }
    }
}