using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class ReportCard : View
    {
        public ReportCard(string name) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/ReportCard"));
            AddToClassList("report-card");
        }
    }
}