using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class McpCollectedCard : ReportCard
    {
        public McpCollectedCard() : base(nameof(McpCollectedCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/McpCollectedCard"));
        }
    }
}