namespace Requests.DataStores.Base
{
    public abstract class ServerSendInBackgroundDataStore<T> : HubBasedDataStore<T>
    {
        public ServerSendInBackgroundDataStore()
        {
            EstablishHubConnection();
        }

        ~ServerSendInBackgroundDataStore()
        {
            CloseHubConnection();
        }
    }
}