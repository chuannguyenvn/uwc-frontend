using System.Collections;
using System.Collections.Generic;
using Commons.Models;
using Requests.DataStores.Base;

namespace Requests.DataStores.Mcps
{
    public class ListViewStore : ServerSendOnFocusedDataStore<List<McpData>>
    {
        public override IEnumerator CreateRequest()
        {
        }
    }
}