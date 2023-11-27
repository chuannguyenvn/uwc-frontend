using Authentication;

namespace Requests.DataStores.Base
{
    public abstract class ServerSendInBackgroundDataStore<T> : HubBasedDataStore<T>
    {
        public ServerSendInBackgroundDataStore()
        {
            AuthenticationManager.Instance.LoggedIn += EstablishHubConnection;
            AuthenticationManager.Instance.LoggedOut += CloseHubConnection;
        }

        ~ServerSendInBackgroundDataStore()
        {
            CloseHubConnection();
        }
    }
}