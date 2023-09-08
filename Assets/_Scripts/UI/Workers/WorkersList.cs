using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Workers
{
    public class WorkersList : ScrollView
    {
        public WorkersList()
        {
            name = "WorkersList";
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<WorkersList, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : ScrollView.UxmlTraits
        {
        }

        #endregion
    }
}