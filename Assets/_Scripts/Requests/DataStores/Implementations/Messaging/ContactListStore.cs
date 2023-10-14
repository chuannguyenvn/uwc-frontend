using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commons.Communications.Messages;
using Commons.Models;
using Managers;
using Newtonsoft.Json;
using Requests.DataStores.Base;
using UnityEngine;

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
                        OnDataUpdated(response);
                    }
                }
            );
        }
    }
}