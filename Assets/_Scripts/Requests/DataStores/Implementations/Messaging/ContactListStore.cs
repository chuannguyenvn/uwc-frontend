﻿using System.Collections;
using Authentication;
using Commons.Communications.Messages;
using Commons.Endpoints;
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
                        OnDataUpdated(response);
                    }
                }
            );
        }
    }
}