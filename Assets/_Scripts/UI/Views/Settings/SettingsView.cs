﻿using System;
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
        private RegisterForFacialRecognitionView _registerForFacialRecognitionView;

        public SettingsView() : base(nameof(SettingsView))
        {
            ConfigureUss(nameof(SettingsView));

            AddToClassList(Configs.IS_DESKTOP ? "side-view" : "full-view");

            DataStoreManager.Setting.Settings.DataUpdated += data => Setting = data.Setting;

            CreateSettings();
            CreateNotificationSettings();
            CreateAccountSettings();
            CreateRegisterForFacialRecognitionView();

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

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.DARK_MODE), () => Setting.DarkMode.ToString(),
                new List<string>()
                {
                    Sentence.SettingsView.ON,
                    Sentence.SettingsView.OFF,
                },
                new Dictionary<string, Action>
                {
                    { ToggleOption.On.ToString(), () => Setting.DarkMode = ToggleOption.On },
                    { ToggleOption.Off.ToString(), () => Setting.DarkMode = ToggleOption.Off },
                }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.COLORBLIND_MODE),
                () => Setting.ColorblindMode.ToString(), new List<string>()
                {
                    Sentence.SettingsView.ON,
                    Sentence.SettingsView.OFF,
                }, new Dictionary<string, Action>
                {
                    { ToggleOption.On.ToString(), () => Setting.ColorblindMode = ToggleOption.On },
                    { ToggleOption.Off.ToString(), () => Setting.ColorblindMode = ToggleOption.Off },
                }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.REDUCE_MOTION),
                () => Setting.ReducedMotionMode.ToString(), new List<string>()
                {
                    Sentence.SettingsView.ON,
                    Sentence.SettingsView.OFF,
                }, new Dictionary<string, Action>
                {
                    { ToggleOption.On.ToString(), () => Setting.ReducedMotionMode = ToggleOption.On },
                    { ToggleOption.Off.ToString(), () => Setting.ReducedMotionMode = ToggleOption.Off },
                }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.LANGUAGE), () => Setting.Language.ToString(),
                new List<string>()
                {
                    Sentence.SettingsView.ENGLISH,
                    Sentence.SettingsView.VIETNAMESE,
                },
                new Dictionary<string, Action>
                {
                    {
                        LanguageOption.English.ToString(), () =>
                        {
                            Setting.Language = Localization.LanguageOption = LanguageOption.English;
                            PlayerPrefs.SetString("Language", LanguageOption.English.ToString());
                        }
                    },
                    {
                        LanguageOption.Vietnamese.ToString(), () =>
                        {
                            Setting.Language = Localization.LanguageOption = LanguageOption.Vietnamese;
                            PlayerPrefs.SetString("Language", LanguageOption.Vietnamese.ToString());
                        }
                    },
                }));
        }

        private void CreateNotificationSettings()
        {
            _settingList.Add(new SectionHeader(Localization.GetSentence(Sentence.SettingsView.NOTIFICATION_SETTINGS)));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.MESSAGES), () => Setting.Messages.ToString(),
                new List<string>()
                {
                    Sentence.SettingsView.ON,
                    Sentence.SettingsView.OFF,
                }, new Dictionary<string, Action>
                {
                    { Sentence.SettingsView.ON, () => Setting.Messages = ToggleOption.On },
                    { Sentence.SettingsView.OFF, () => Setting.Messages = ToggleOption.Off },
                }));

            if (Configs.IS_DESKTOP)
            {
                _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.EMPLOYEES_LOGGED_IN),
                    () => Setting.EmployeesLoggedIn.ToString(),
                    new List<string>()
                    {
                        Sentence.SettingsView.ON,
                        Sentence.SettingsView.OFF,
                    }, new Dictionary<string, Action>
                    {
                        { Sentence.SettingsView.ON, () => Setting.EmployeesLoggedIn = ToggleOption.On },
                        { Sentence.SettingsView.OFF, () => Setting.EmployeesLoggedIn = ToggleOption.Off },
                    }));

                _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.EMPLOYEES_LOGGED_OUT),
                    () => Setting.EmployeesLoggedOut.ToString(),
                    new List<string>()
                    {
                        Sentence.SettingsView.ON,
                        Sentence.SettingsView.OFF,
                    }, new Dictionary<string, Action>
                    {
                        { Sentence.SettingsView.ON, () => Setting.EmployeesLoggedOut = ToggleOption.On },
                        { Sentence.SettingsView.OFF, () => Setting.EmployeesLoggedOut = ToggleOption.Off },
                    }));
            }

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.MCPS_ALMOST_FULL),
                () => Setting.McpsAlmostFull.ToString(), new List<string>()
                {
                    Sentence.SettingsView.ON,
                    Sentence.SettingsView.OFF,
                }, new Dictionary<string, Action>
                {
                    { ToggleOption.On.ToString(), () => Setting.McpsAlmostFull = ToggleOption.On },
                    { ToggleOption.Off.ToString(), () => Setting.McpsAlmostFull = ToggleOption.Off },
                }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.MCPS_FULL), () => Setting.McpsFull.ToString(),
                new List<string>()
                {
                    Sentence.SettingsView.ON,
                    Sentence.SettingsView.OFF,
                }, new Dictionary<string, Action>
                {
                    { ToggleOption.On.ToString(), () => Setting.McpsFull = ToggleOption.On },
                    { ToggleOption.Off.ToString(), () => Setting.McpsFull = ToggleOption.Off },
                }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.MCPS_EMPTIED),
                () => Setting.McpsEmptied.ToString(), new List<string>()
                {
                    Sentence.SettingsView.ON,
                    Sentence.SettingsView.OFF,
                }, new Dictionary<string, Action>
                {
                    { ToggleOption.On.ToString(), () => Setting.McpsEmptied = ToggleOption.On },
                    { ToggleOption.Off.ToString(), () => Setting.McpsEmptied = ToggleOption.Off },
                }));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.SOFTWARE_UPDATE_AVAILABLE),
                () => Setting.SoftwareUpdateAvailable.ToString(),
                new List<string>()
                {
                    Sentence.SettingsView.ON,
                    Sentence.SettingsView.OFF,
                }, new Dictionary<string, Action>
                {
                    { ToggleOption.On.ToString(), () => Setting.SoftwareUpdateAvailable = ToggleOption.On },
                    { ToggleOption.Off.ToString(), () => Setting.SoftwareUpdateAvailable = ToggleOption.Off },
                }));
        }

        private void CreateAccountSettings()
        {
            _settingList.Add(new SectionHeader(Localization.GetSentence(Sentence.SettingsView.ACCOUNT_SETTINGS)));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.SettingsView.ONLINE_STATUS),
                () => Setting.OnlineStatus.ToString(), new List<string>()
                {
                    Sentence.SettingsView.ONLINE,
                    Sentence.SettingsView.OFFLINE,
                }, new Dictionary<string, Action>
                {
                    { OnlineStatusOption.Online.ToString(), () => { Setting.OnlineStatus = OnlineStatusOption.Online; } },
                    { OnlineStatusOption.Offline.ToString(), () => { Setting.OnlineStatus = OnlineStatusOption.Offline; } },
                }));

            if (Configs.IS_DESKTOP)
            {
                _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.EXPORT_WORK_LOGS), () => { }));
                _settingList.Add(new TriggerSettingListEntry("Export work logs", () => { }));
            }

            _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.CHANGE_PERSONAL_INFORMATION), () => { }));

            _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.CHANGE_PASSWORD), () => { }));

            if (!Configs.IS_DESKTOP)
                _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.REGISTER_FACIAL_RECOGNITION),
                    () => _registerForFacialRecognitionView.Show()));

            _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.REPORT_PROBLEM), () => { }));

            _settingList.Add(new TriggerSettingListEntry(Localization.GetSentence(Sentence.SettingsView.LOGOUT), () => { }, true));
        }

        private void CreateRegisterForFacialRecognitionView()
        {
            _registerForFacialRecognitionView = new RegisterForFacialRecognitionView();
            Add(_registerForFacialRecognitionView);

            _registerForFacialRecognitionView.Hide();
        }

        public override void FocusView()
        {
            DataStoreManager.Setting.Settings.Focus();
            DataStoreManager.Setting.Settings.DataUpdated += _ => { _settingList.style.display = DisplayStyle.Flex; };
        }

        public override void UnfocusView()
        {
            DataStoreManager.Setting.Settings.Unfocus();
            _settingList.style.display = DisplayStyle.None;
        }
    }
}