using Commons.Models;
using UI.Base;
using UI.Base.ListView;

namespace UI.Views.Workers
{
    public class WorkersView : ListView<UserProfile>
    {
        public WorkersView() : base(nameof(WorkersView), false)
        {
        }
    }
}