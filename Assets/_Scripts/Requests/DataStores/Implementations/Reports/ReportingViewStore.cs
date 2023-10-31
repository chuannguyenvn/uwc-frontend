using System.Collections;
using Commons.Communications.Reports;
using Commons.Endpoints;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Reports
{
    public class ReportingViewStore : ServerSendOnFocusedDataStore<GetDashboardReportResponse>
    {
        protected override IEnumerator CreateRequest()
        {
            yield return RequestHelper.SendGetRequest<GetDashboardReportResponse>(
                Endpoints.Report.Get,
                (success, response) =>
                {
                    if (success)
                    {
                        OnDataUpdated(response);
                    }
                }
            );
        }
    }
}