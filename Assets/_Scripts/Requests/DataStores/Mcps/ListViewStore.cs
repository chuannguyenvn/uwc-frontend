using System.Collections;
using System.Collections.Generic;
using Commons.Communications.Mcps;
using Commons.Models;
using Commons.Types;
using Requests.DataStores.Base;

namespace Requests.DataStores.Mcps
{
    public class ListViewStore : ServerSendOnFocusedDataStore<List<McpData>>
    {
        public override IEnumerator CreateRequest()
        {
            yield return RequestHelper.SendPostRequest<GetMcpDataResponse>(
                Endpoints.McpData.GET,
                new McpDataQueryParameters()
                {
                },
                (success, response) =>
                {
                    if (success)
                    {
                        OnDataUpdated(response.Results);
                    }
                });
        }
    }
}