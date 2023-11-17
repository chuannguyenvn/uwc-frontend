using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkersView : View
    {
        private ScrollView _scrollView;

        public WorkersView() : base(nameof(WorkersView))
        {
            ConfigureUss(nameof(WorkersView));

            AddToClassList("side-view");

            CreateScrollView();
            CreateEntries();
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);
        }

        private void CreateEntries()
        {
            for (int i = 0; i < 30; i++)
            {
                _scrollView.Add(new WorkerListEntry(new UserProfile()
                {
                    FirstName = "Worker name",
                    Address = "Worker address"
                }));
            }
        }
    }
}