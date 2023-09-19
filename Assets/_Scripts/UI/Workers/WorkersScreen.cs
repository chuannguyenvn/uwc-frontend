using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Workers
{
    public class WorkersScreen : VisualElement
    {
        public WorkersScreen()
        {
            name = "WorkersScreen";
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