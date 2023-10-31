using Commons.Communications.Reports;
using Requests;
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

        public McpCollectedCard McpCollectedCard;
        public LogisticCard LogisticCard;
        public WeatherCard WeatherCard;
        public McpCapacityCard McpCapacityCard;
        public TaskCard TaskCard;
        public WorkerCard WorkerCard;
        public GraphCard GraphCard;

        public ReportingView() : base(nameof(ReportingView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/ReportingView"));
            AddToClassList("full-view");

            FirstCardRowContainer = new VisualElement { name = "FirstCardRowContainer" };
            FirstCardRowContainer.AddToClassList("report-card-row-container");
            Add(FirstCardRowContainer);

            McpCollectedCard = new McpCollectedCard();
            LogisticCard = new LogisticCard();
            WeatherCard = new WeatherCard();

            FirstCardRowContainer.Add(McpCollectedCard);
            FirstCardRowContainer.Add(LogisticCard);
            FirstCardRowContainer.Add(WeatherCard);

            SecondCardRowContainer = new VisualElement { name = "SecondCardRowContainer" };
            SecondCardRowContainer.AddToClassList("report-card-row-container");
            Add(SecondCardRowContainer);

            McpCapacityCard = new McpCapacityCard();
            TaskCard = new TaskCard();
            WorkerCard = new WorkerCard();

            SecondCardRowContainer.Add(McpCapacityCard);
            SecondCardRowContainer.Add(TaskCard);
            SecondCardRowContainer.Add(WorkerCard);

            GraphContainer = new VisualElement { name = "GraphContainer" };
            GraphContainer.AddToClassList("graph-container");
            Add(GraphContainer);

            GraphCard = new GraphCard();
            GraphContainer.Add(GraphCard);

            DataStoreManager.Reporting.ReportingView.DataUpdated += DataUpdatedHandler;
        }

        ~ReportingView()
        {
            DataStoreManager.Reporting.ReportingView.DataUpdated -= DataUpdatedHandler;
        }

        private void DataUpdatedHandler(GetDashboardReportResponse response)
        {
            McpCollectedCard.UpdateData(response);
            LogisticCard.UpdateData(response);
            WeatherCard.UpdateData(response);
            McpCapacityCard.UpdateData(response);
            TaskCard.UpdateData(response);
            WorkerCard.UpdateData(response);
            GraphCard.UpdateData(response);
        }
        
        public override void FocusView()
        {
            DataStoreManager.Reporting.ReportingView.Focus();
        }
        
        public override void UnfocusView()
        {
            DataStoreManager.Reporting.ReportingView.Unfocus();
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