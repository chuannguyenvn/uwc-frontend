using Commons.Communications.Reports;
using UI.Base;

namespace UI.Views.Reports.Cards
{
    public abstract class ReportCard : View
    {
        protected ReportCard(string name) : base(name)
        {
            ConfigureUss(nameof(ReportCard));
        }

        public abstract void UpdateData(GetDashboardReportResponse response);
    }
}