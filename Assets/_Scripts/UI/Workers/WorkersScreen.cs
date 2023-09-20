using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Workers
{
    public class WorkersScreen : VisualElement
    {
        private WorkersDataList _workersDataList;
        
        public WorkersScreen()
        {
            name = "WorkersScreen";
            
            _workersDataList = new WorkersDataList();
            Add(_workersDataList);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<WorkersScreen, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}