using System;
using System.Collections;
using Commons.Communications.Vehicles;
using Commons.Endpoints;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Vehicles
{
    public class AllVehicleListStore : ServerSendOnFocusedDataStore<GetAllVehicleResponse>
    {
        protected override IEnumerator CreateRequest(Action callback)
        {
            yield return RequestHelper.SendGetRequest<GetAllVehicleResponse>(
                Endpoints.VehicleData.GetAll,
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