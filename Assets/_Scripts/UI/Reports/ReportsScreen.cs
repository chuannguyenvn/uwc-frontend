using UI.Commons;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Reports
{
    public class ReportsScreen : FullScreenPanel
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
        public new class UxmlTraits : FullScreenPanel.UxmlTraits
        {
        }

        #endregion
    }
}