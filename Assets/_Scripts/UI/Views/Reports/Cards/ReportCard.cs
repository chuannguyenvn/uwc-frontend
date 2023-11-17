using Commons.Communications.Reports;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public abstract class ReportCard : View
    {
        protected ReportCard(string name) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/ReportCard"));
            AddToClassList("report-card");
        }

        public abstract void UpdateData(GetDashboardReportResponse response);
    }
}