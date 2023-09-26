using UI.Common;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Reporting
{
    public class ReportingView : FullScreenView
    {
        public ReportingView() : base("Reporting")
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<ReportingView, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}