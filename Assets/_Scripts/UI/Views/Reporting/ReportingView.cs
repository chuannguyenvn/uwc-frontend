using UI.Common;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Reporting
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