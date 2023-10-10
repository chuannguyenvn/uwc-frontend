using Commons.Communications.Mcps;
using Commons.Types;
using UnityEngine;

namespace Requests.DataStores.Mcps
{
    public class ListViewWatcher : DataWatcher
    {
        public readonly McpDataQueryParameters QueryParameters = new();
        public GetMcpDataResponse Response = new();

        public ListViewWatcher()
        {
            Request = new ParameterizedRequest<GetMcpDataResponse>(
                Endpoints.McpData.GET,
                RequestType.POST,
                QueryParameters,
                (success, response) =>
                {
                    if (success)
                    {
                        Response = response;
                        DataRequested?.Invoke();
                    }
                    else
                    {
                        Debug.LogError("Failed to get Mcp data");
                    }
                });
        }
    }
}