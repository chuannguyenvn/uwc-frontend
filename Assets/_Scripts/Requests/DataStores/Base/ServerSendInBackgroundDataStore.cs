using Authentication;

namespace Requests.DataStores.Base
{
    public abstract class ServerSendInBackgroundDataStore<T> : HubBasedDataStore<T>
    {
        public ServerSendInBackgroundDataStore()
        {
            AuthenticationManager.LoggedIn += EstablishHubConnection;
            AuthenticationManager.LoggedOut += CloseHubConnection;
        }

        ~ServerSendInBackgroundDataStore()
        {
            CloseHubConnection();
        }
    }
}