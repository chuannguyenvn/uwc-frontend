using Commons.Models;
using UI.Base.ListView;

namespace UI.Views.Workers
{
    public class WorkerDataEntry : ListEntry<UserProfile>
    {
        public WorkerDataEntry(string name) : base(name)
        {
            SetPrimaryText("Worker name");
            SetSecondaryText("Worker status");
        }
    }
}