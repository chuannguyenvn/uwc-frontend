using System.Collections;
using Commons.Communications.UserProfiles;
using Commons.Endpoints;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.UserProfile
{
    public class AllWorkerProfileListStore : ServerSendOnFocusedDataStore<GetAllWorkerProfilesResponse>
    {
        protected override IEnumerator CreateRequest()
        {
            yield return RequestHelper.SendGetRequest<GetAllWorkerProfilesResponse>(
                Endpoints.UserProfile.GetAllWorkerProfiles,
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