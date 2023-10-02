using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkersView : View
    {
        private ScrollView _scrollView;

        public WorkersView() : base(nameof(WorkersView), false)
        {
            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);
            
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