using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class SettingListEntry : AdaptiveElement
    {
        public TextElement SettingNameText;
        
        public SettingListEntry(string name) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Settings/SettingListEntry"));
            AddToClassList("setting-list-entry");

            SettingNameText = new TextElement { name = "SettingNameText" };
            SettingNameText.text = name;
            SettingNameText.AddToClassList("normal-text");
            Add(SettingNameText);
        }
    }
}