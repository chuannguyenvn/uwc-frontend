using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Reports
{
    public class ReportsScreen : VisualElement
    {
        public ReportsScreen()
        {
            name = "ReportsScreen";
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<ReportsScreen, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}