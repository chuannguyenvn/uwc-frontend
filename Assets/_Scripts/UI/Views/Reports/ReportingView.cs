using UI.Base;
using UI.Views.Reports.Cards;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Reports
{
    public class ReportingView : View
    {
        public VisualElement FirstCardRowContainer;
        public VisualElement SecondCardRowContainer;
        public VisualElement GraphContainer;

        public ReportingView() : base(nameof(ReportingView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/ReportingView"));
            AddToClassList("full-view");

            FirstCardRowContainer = new VisualElement { name = "FirstCardRowContainer" };
            FirstCardRowContainer.AddToClassList("report-card-row-container");
            Add(FirstCardRowContainer);

            var mcpCollectedCard = new McpCollectedCard();
            var logisticCard = new LogisticCard();
            var weatherCard = new WeatherCard();

            FirstCardRowContainer.Add(mcpCollectedCard);
            FirstCardRowContainer.Add(logisticCard);
            FirstCardRowContainer.Add(weatherCard);

            SecondCardRowContainer = new VisualElement { name = "SecondCardRowContainer" };
            SecondCardRowContainer.AddToClassList("report-card-row-container");
            Add(SecondCardRowContainer);

            var mcpCapacityCard = new McpCapacityCard();
            var taskCard = new TaskCard();
            var workerCard = new WorkerCard();

            SecondCardRowContainer.Add(mcpCapacityCard);
            SecondCardRowContainer.Add(taskCard);
            SecondCardRowContainer.Add(workerCard);

            GraphContainer = new VisualElement { name = "GraphContainer" };
            GraphContainer.AddToClassList("graph-container");
            Add(GraphContainer);

            var graphCard = new GraphCard();
            GraphContainer.Add(graphCard);
        }
    }

    #region UXML

    [Preserve]
    public class UxmlFactory : UxmlFactory<ReportingView, UxmlTraits>
    {
    }

    [Preserve]
    public class UxmlTraits : VisualElement.UxmlTraits
    {
    }

    #endregion
}