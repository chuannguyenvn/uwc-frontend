using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class SettingList : AdaptiveElement
    {
        public ScrollView ScrollView;

        public SettingList() : base(nameof(SettingList))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Settings/SettingList"));

            ScrollView = new ScrollView();
            ScrollView.AddToClassList("list-view");
            Add(ScrollView);
        }

        public void Add(SettingListEntry entry)
        {
            ScrollView.Add(entry);
        }
        
        public void Add(SectionHeader header)
        {
            ScrollView.Add(header);
        }
    }
}