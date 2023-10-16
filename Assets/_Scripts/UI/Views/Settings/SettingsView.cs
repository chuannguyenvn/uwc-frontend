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
        public SettingList SettingList;

        public SettingsView() : base(nameof(SettingsView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Settings/SettingsView"));
            AddToClassList(Configs.IS_DESKTOP ? "side-view" : "full-view");

            SettingList = new SettingList();
            Add(SettingList);

            CreateSettings();
            CreateNotificationSettings();
            CreateAccountSettings();
        }

        private void CreateSettings()
        {
            CreateInterfaceSettings();
        }

        private void CreateInterfaceSettings()
        {
            SettingList.Add(new SectionHeader("Interface Settings"));

            SettingList.Add(new ChoiceSettingListEntry("Dark mode", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            SettingList.Add(new ChoiceSettingListEntry("Colorblind mode", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            SettingList.Add(new ChoiceSettingListEntry("Reduce motion", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            SettingList.Add(new ChoiceSettingListEntry("Language", new Dictionary<string, Action>
            {
                { "English", () => { } },
                { "Vietnamese", () => { } },
            }));
        }

        private void CreateNotificationSettings()
        {
            SettingList.Add(new SectionHeader("Notification Settings"));

            SettingList.Add(new ChoiceSettingListEntry("Messages", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            if (Configs.IS_DESKTOP)
            {
                SettingList.Add(new ChoiceSettingListEntry("Employees logged in", new Dictionary<string, Action>
                {
                    { "On", () => { } },
                    { "Off", () => { } },
                }));

                SettingList.Add(new ChoiceSettingListEntry("Employees logged out", new Dictionary<string, Action>
                {
                    { "On", () => { } },
                    { "Off", () => { } },
                }));
            }

            SettingList.Add(new ChoiceSettingListEntry("MCPs almost full", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            SettingList.Add(new ChoiceSettingListEntry("MCPs full", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            SettingList.Add(new ChoiceSettingListEntry("MCPs emptied", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));

            SettingList.Add(new ChoiceSettingListEntry("Software update available", new Dictionary<string, Action>
            {
                { "On", () => { } },
                { "Off", () => { } },
            }));
        }

        private void CreateAccountSettings()
        {
            SettingList.Add(new SectionHeader("Account Settings"));

            SettingList.Add(new ChoiceSettingListEntry("Online status", new Dictionary<string, Action>
            {
                { "Online", () => { } },
                { "Offline", () => { } },
            }));

            if (Configs.IS_DESKTOP)
            {
                SettingList.Add(new TriggerSettingListEntry("Export messages", () => { }));
                SettingList.Add(new TriggerSettingListEntry("Export work logs", () => { }));
            }

            SettingList.Add(new TriggerSettingListEntry("Change personal information", () => { }));

            SettingList.Add(new TriggerSettingListEntry("Change password", () => { }));

            if (!Configs.IS_DESKTOP) SettingList.Add(new TriggerSettingListEntry("Register facial recognition", () => { }));

            SettingList.Add(new TriggerSettingListEntry("Report problem", () => { }));
            
            SettingList.Add(new TriggerSettingListEntry("Logout", () => { }, true));
        }
    }
}