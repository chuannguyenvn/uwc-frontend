using Commons.Models;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkersView : View
    {
        private ScrollView _scrollView;

        public WorkersView() : base(nameof(WorkersView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Workers/WorkersView"));
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Workers/WorkerListEntry"));
            AddToClassList("side-view");
            
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