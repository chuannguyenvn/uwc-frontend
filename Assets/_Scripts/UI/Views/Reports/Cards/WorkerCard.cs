using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class WorkerCard : ReportCard
    {
        public WorkerCard() : base(nameof(WorkerCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/WorkerCard"));
        }
    }
}