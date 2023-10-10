using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Commons.Communications.Mcps;
using Commons.Types;
using UnityEngine;

namespace Requests.DataStores.Mcps
{
    public class ListView : DataStore
    {
        public readonly McpDataQueryParameters QueryParameters = new();
        public GetMcpDataResponse Response = new();

        public ListView()
        {
            Request = new ParameterizedRequest<GetMcpDataResponse>(
                Endpoints.McpData.GET,
                RequestType.POST,
                QueryParameters,
                (success, response) =>
                {
                    if (success)
                    {
                        Debug.Log("Got Mcp data");
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