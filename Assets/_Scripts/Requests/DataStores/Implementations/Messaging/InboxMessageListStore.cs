using System.Collections;
using Commons.Communications.Messages;
using Managers;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Messaging
{
    public class InboxMessageListStore : ServerSendOnFocusedDataStore<GetMessagesBetweenTwoUsersResponse>
    {
        public int OtherUserAccountId { get; set; }

        protected override IEnumerator CreateRequest()
        {
            yield return RequestHelper.SendPostRequest<GetMessagesBetweenTwoUsersResponse>(
                Endpoints.Messaging.GetMessagesBetweenTwoUsers,
                new GetMessagesBetweenTwoUsersRequest()
                {
                    UserAccountId = AuthenticationManager.Instance.UserAccountId,
                    OtherUserAccountId = OtherUserAccountId,
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