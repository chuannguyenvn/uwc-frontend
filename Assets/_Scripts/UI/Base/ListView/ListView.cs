using UnityEngine.UIElements;

namespace UI.Base.ListView
{
    public class ListView<T> : View
    {
        private ScrollView _scrollView;

        public ListView(string name, bool isFullScreen) : base(name, isFullScreen)
        {
            _scrollView = new ScrollView { name = "ScrollView" };
            Add(_scrollView);
        }

        public void AddEntry(ListEntry<T> entry)
        {
            _scrollView.Add(entry);
        }
    }
}