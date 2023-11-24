using System;
using System.Collections;
using Authentication;
using Commons.Communications.Settings;
using Commons.Endpoints;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Setting
{
    public class SettingStore : ServerSendOnFocusedDataStore<GetSettingResponse>
    {
        protected override IEnumerator CreateRequest(Action callback)
        {
            yield return RequestHelper.SendPostRequest<GetSettingResponse>(
                Endpoints.Setting.GetSetting,
                new GetSettingRequest()
                {
                    AccountId = AuthenticationManager.Instance.UserAccountId,
                },
                (success, response) =>
                {
                    if (success)
                    {
                        OnDataUpdated(response);
                        callback?.Invoke();
                    }
                }
            );
        }
    }
}