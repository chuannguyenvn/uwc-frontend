using System.Collections;
using Commons.Communications.Messages;
using Managers;
using Newtonsoft.Json;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Messaging
{
    public class ContactListStore : ServerSendOnFocusedDataStore<GetPreviewMessagesResponse>
    {
        protected override IEnumerator CreateRequest()
        {
            yield return RequestHelper.SendPostRequest<GetPreviewMessagesResponse>(
                Endpoints.Messaging.GetPreviewMessages,
                new GetPreviewMessagesRequest()
                {
                    UserAccountId = AuthenticationManager.Instance.UserAccountId
                },
                (success, response) =>
                {
                    if (success)
                    {
                        GetPreviewMessagesResponse getPreviewMessagesResponse = new GetPreviewMessagesResponse();
                        JsonConvert.PopulateObject(response, getPreviewMessagesResponse);
                        OnDataUpdated(getPreviewMessagesResponse);
                    }
                }
            );
        }
    }
}