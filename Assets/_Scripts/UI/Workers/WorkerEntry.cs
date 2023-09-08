using UI.Commons;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Workers
{
    public class WorkerEntry : ListEntry
    {
        public WorkerEntry()
        {
            name = "WorkerEntry";
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<WorkerEntry, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}