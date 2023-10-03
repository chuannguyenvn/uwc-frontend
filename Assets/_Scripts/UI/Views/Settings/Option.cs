using System;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace UI.Views.Settings
{
    public class Option : AdaptiveElement
    {
        public TextElement SettingNameText;

        public Option(string name, Action callback) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Settings/Option"));
            AddToClassList("option");

            SettingNameText = new TextElement();
            SettingNameText.AddToClassList("normal-text");
            SettingNameText.text = name;
            Add(SettingNameText);

            RegisterCallback<MouseUpEvent>(_ => callback?.Invoke());
        }
        
        public void Activate()
        {
            SettingNameText.RemoveFromClassList("deactivated");
            SettingNameText.AddToClassList("activated");
        }
        
        public void Deactivate()
        {
            SettingNameText.RemoveFromClassList("activated");
            SettingNameText.AddToClassList("deactivated");
        }
    }
}