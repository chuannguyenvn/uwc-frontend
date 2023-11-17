using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class SettingList : AdaptiveElement
    {
        private ScrollView _scrollView;

        public SettingList() : base(nameof(SettingList))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Settings/SettingList"));

            CreateScrollView();
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);
        }

        public void Add(SettingListEntry entry)
        {
            _scrollView.Add(entry);
        }

        public void Add(SectionHeader header)
        {
            _scrollView.Add(header);
        }
    }
}