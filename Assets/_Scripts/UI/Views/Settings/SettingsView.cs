using System;
using System.Collections.Generic;
using Commons.Models;
using Commons.Types.SettingOptions;
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
            _settingList.Add(new SectionHeader("Interface Settings"));

            _settingList.Add(new ChoiceSettingListEntry("Dark mode", () => Setting.DarkMode.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.DarkMode = ToggleOption.On },
                { "Off", () => Setting.DarkMode = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry("Colorblind mode", () => Setting.ColorblindMode.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.ColorblindMode = ToggleOption.On },
                { "Off", () => Setting.ColorblindMode = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry("Reduce motion", () => Setting.ReducedMotionMode.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.ReducedMotionMode = ToggleOption.On },
                { "Off", () => Setting.ReducedMotionMode = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry("Language", () => Setting.Language.ToString(), new Dictionary<string, Action>
            {
                {
                    "English", () => { Setting.Language = Localization.Localization.LanguageOption = LanguageOption.English; }
                },
                {
                    "Vietnamese", () => { Setting.Language = Localization.Localization.LanguageOption = LanguageOption.Vietnamese; }
                },
            }));
        }

        private void CreateNotificationSettings()
        {
            _settingList.Add(new SectionHeader("Notification Settings"));

            _settingList.Add(new ChoiceSettingListEntry("Messages", () => Setting.Messages.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.Messages = ToggleOption.On },
                { "Off", () => Setting.Messages = ToggleOption.Off },
            }));

            if (Configs.IS_DESKTOP)
            {
                _settingList.Add(new ChoiceSettingListEntry("Employees logged in", () => Setting.EmployeesLoggedIn.ToString(),
                    new Dictionary<string, Action>
                    {
                        { "On", () => Setting.EmployeesLoggedIn = ToggleOption.On },
                        { "Off", () => Setting.EmployeesLoggedIn = ToggleOption.Off },
                    }));

                _settingList.Add(new ChoiceSettingListEntry("Employees logged out", () => Setting.EmployeesLoggedOut.ToString(),
                    new Dictionary<string, Action>
                    {
                        { "On", () => Setting.EmployeesLoggedOut = ToggleOption.On },
                        { "Off", () => Setting.EmployeesLoggedOut = ToggleOption.Off },
                    }));
            }

            _settingList.Add(new ChoiceSettingListEntry("MCPs almost full", () => Setting.McpsAlmostFull.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.McpsAlmostFull = ToggleOption.On },
                { "Off", () => Setting.McpsAlmostFull = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry("MCPs full", () => Setting.McpsFull.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.McpsFull = ToggleOption.On },
                { "Off", () => Setting.McpsFull = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry("MCPs emptied", () => Setting.McpsEmptied.ToString(), new Dictionary<string, Action>
            {
                { "On", () => Setting.McpsEmptied = ToggleOption.On },
                { "Off", () => Setting.McpsEmptied = ToggleOption.Off },
            }));

            _settingList.Add(new ChoiceSettingListEntry("Software update available", () => Setting.SoftwareUpdateAvailable.ToString(),
                new Dictionary<string, Action>
                {
                    { "On", () => Setting.SoftwareUpdateAvailable = ToggleOption.On },
                    { "Off", () => Setting.SoftwareUpdateAvailable = ToggleOption.Off },
                }));
        }

        private void CreateAccountSettings()
        {
            _settingList.Add(new SectionHeader("Account Settings"));

            _settingList.Add(new ChoiceSettingListEntry("Online status", () => Setting.OnlineStatus.ToString(), new Dictionary<string, Action>
            {
                { "Online", () => { Setting.OnlineStatus = OnlineStatusOption.Online; } },
                { "Offline", () => { Setting.OnlineStatus = OnlineStatusOption.Offline; } },
            }));

            if (Configs.IS_DESKTOP)
            {
                _settingList.Add(new TriggerSettingListEntry("Export messages", () => { }));
                _settingList.Add(new TriggerSettingListEntry("Export work logs", () => { }));
            }

            _settingList.Add(new TriggerSettingListEntry("Change personal information", () => { }));

            _settingList.Add(new TriggerSettingListEntry("Change password", () => { }));

            if (!Configs.IS_DESKTOP) _settingList.Add(new TriggerSettingListEntry("Register facial recognition", () => { }));

            _settingList.Add(new TriggerSettingListEntry("Report problem", () => { }));

            _settingList.Add(new TriggerSettingListEntry("Logout", () => { }, true));
        }

        public override void FocusView()
        {
            DataStoreManager.Setting.Settings.Focus();
            DataStoreManager.Setting.Settings.DataUpdated += _ =>
            {
                _settingList.style.display = DisplayStyle.Flex;
                Debug.Log("Show");
            };
        }

        public override void UnfocusView()
        {
            DataStoreManager.Setting.Settings.Unfocus();
            _settingList.style.display = DisplayStyle.None;
        }
    }
}