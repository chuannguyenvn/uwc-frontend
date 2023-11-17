using Commons.Communications.Reports;
using Requests;
using UI.Base;
using UI.Views.Reports.Cards;
using UnityEngine.UIElements;

namespace UI.Views.Reports
{
    public class ReportingView : View
    {
        // First row
        private VisualElement _firstCardRowContainer;
        private McpCollectedCard _mcpCollectedCard;
        private LogisticCard _logisticCard;
        private WeatherCard _weatherCard;

        // Second row
        private VisualElement _secondCardRowContainer;
        private McpCapacityCard _mcpCapacityCard;
        private TaskCard _taskCard;
        private WorkerCard _workerCard;

        // Graph
        private VisualElement _graphContainer;
        private GraphCard _graphCard;

        public ReportingView() : base(nameof(ReportingView))
        {
            ConfigureUss(nameof(ReportingView));

            AddToClassList("full-view");

            CreateFirstRow();
            CreateSecondRow();
            CreateGraph();

            DataStoreManager.Reporting.ReportingView.DataUpdated += DataUpdatedHandler;
        }

        ~ReportingView()
        {
            DataStoreManager.Reporting.ReportingView.DataUpdated -= DataUpdatedHandler;
        }

        private void CreateGraph()
        {
            _graphContainer = new VisualElement { name = "GraphContainer" };
            _graphContainer.AddToClassList("graph-container");
            Add(_graphContainer);

            _graphCard = new GraphCard();
            _graphContainer.Add(_graphCard);
        }

        private void CreateSecondRow()
        {
            _secondCardRowContainer = new VisualElement { name = "SecondCardRowContainer" };
            _secondCardRowContainer.AddToClassList("report-card-row-container");
            Add(_secondCardRowContainer);

            _mcpCapacityCard = new McpCapacityCard();
            _taskCard = new TaskCard();
            _workerCard = new WorkerCard();

            _secondCardRowContainer.Add(_mcpCapacityCard);
            _secondCardRowContainer.Add(_taskCard);
            _secondCardRowContainer.Add(_workerCard);
        }

        private void CreateFirstRow()
        {
            _firstCardRowContainer = new VisualElement { name = "FirstCardRowContainer" };
            _firstCardRowContainer.AddToClassList("report-card-row-container");
            Add(_firstCardRowContainer);

            _mcpCollectedCard = new McpCollectedCard();
            _logisticCard = new LogisticCard();
            _weatherCard = new WeatherCard();

            _firstCardRowContainer.Add(_mcpCollectedCard);
            _firstCardRowContainer.Add(_logisticCard);
            _firstCardRowContainer.Add(_weatherCard);
        }

        private void DataUpdatedHandler(GetDashboardReportResponse response)
        {
            _mcpCollectedCard.UpdateData(response);
            _logisticCard.UpdateData(response);
            _weatherCard.UpdateData(response);
            _mcpCapacityCard.UpdateData(response);
            _taskCard.UpdateData(response);
            _workerCard.UpdateData(response);
            _graphCard.UpdateData(response);
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
}