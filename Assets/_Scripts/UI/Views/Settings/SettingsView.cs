using System;
using System.Collections.Generic;
using Settings;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class SettingsView : View
    {
        private SettingList _settingList;

        public SettingsView() : base(nameof(SettingsView))
        {
            ConfigureUss(nameof(SettingsView));

            AddToClassList(Configs.IS_DESKTOP ? "side-view" : "full-view");

            CreateSettings();
            CreateNotificationSettings();
            CreateAccountSettings();
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

            _settingList.Add(new ChoiceSettingListEntry("Dark mode", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            _settingList.Add(new ChoiceSettingListEntry("Colorblind mode", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            _settingList.Add(new ChoiceSettingListEntry("Reduce motion", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            _settingList.Add(new ChoiceSettingListEntry("Language", new Dictionary<string, Action>
            {
                { "English", () => { } },
                { "Vietnamese", () => { } },
            }));
        }

        private void CreateNotificationSettings()
        {
            _settingList.Add(new SectionHeader("Notification Settings"));

            _settingList.Add(new ChoiceSettingListEntry("Messages", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            if (Configs.IS_DESKTOP)
            {
                _settingList.Add(new ChoiceSettingListEntry("Employees logged in", new Dictionary<string, Action>
                {
                    { "On", () => { } },
                    { "Off", () => { } },
                }));

                _settingList.Add(new ChoiceSettingListEntry("Employees logged out", new Dictionary<string, Action>
                {
                    { "On", () => { } },
                    { "Off", () => { } },
                }));
            }

            _settingList.Add(new ChoiceSettingListEntry("MCPs almost full", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            _settingList.Add(new ChoiceSettingListEntry("MCPs full", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            _settingList.Add(new ChoiceSettingListEntry("MCPs emptied", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            _settingList.Add(new ChoiceSettingListEntry("Software update available", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));
        }

        private void CreateAccountSettings()
        {
            _settingList.Add(new SectionHeader("Account Settings"));

            _settingList.Add(new ChoiceSettingListEntry("Online status", new Dictionary<string, Action>
            {
                { "Online", () => { } },
                { "Offline", () => { } },
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
    }
}