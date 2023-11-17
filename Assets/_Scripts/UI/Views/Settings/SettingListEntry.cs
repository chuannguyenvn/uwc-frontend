using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class SettingListEntry : AdaptiveElement
    {
        protected TextElement SettingNameText;

        protected SettingListEntry(string name) : base(name)
        {
            ConfigureUss(nameof(SettingListEntry));

            CreateSettingNameText(name);
        }

        private void CreateSettingNameText(string name)
        {
            SettingNameText = new TextElement { name = "SettingNameText" };
            SettingNameText.AddToClassList("normal-text");
            SettingNameText.text = name;
            Add(SettingNameText);
        }
    }
}