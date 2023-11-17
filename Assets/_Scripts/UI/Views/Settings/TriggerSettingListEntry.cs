using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class TriggerSettingListEntry : SettingListEntry
    {
        public TriggerSettingListEntry(string name, Action callback, bool isDangerous = false) : base(name)
        {
            ConfigureUss(nameof(TriggerSettingListEntry));

            if (isDangerous) AddToClassList("dangerous");

            RegisterCallback<ClickEvent>(_ => callback?.Invoke());
        }
    }
}