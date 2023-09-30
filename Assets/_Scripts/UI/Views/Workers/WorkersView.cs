using UI.Common;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkersView : View
    
    {
        private WorkersDataList _workersDataList;
        
        public WorkersView() : base("WorkersView")
        {
            _workersDataList = new WorkersDataList();
            Add(_workersDataList);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<WorkersView, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}