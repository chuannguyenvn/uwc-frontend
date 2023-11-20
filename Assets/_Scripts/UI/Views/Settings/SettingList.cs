using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class SettingList : AdaptiveElement
    {
        private ScrollView _scrollView;

        public SettingList() : base(nameof(SettingList))
        {
            ConfigureUss(nameof(SettingList));

            CreateScrollView();
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollView();
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