using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports
{
    public class ReportingView : View
    {
        public ReportingView() : base(nameof(ReportingView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/ReportingView"));
            AddToClassList("full-view");
        }
    }
}