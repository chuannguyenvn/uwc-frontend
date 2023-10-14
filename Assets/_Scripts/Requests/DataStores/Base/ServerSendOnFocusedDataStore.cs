namespace Requests.DataStores.Base
{
    public abstract class ServerSendOnFocusedDataStore<T> : HubBasedDataStore<T>
    {
        public void Focus()
        {
            SendRequest();
            EstablishHubConnection();
        }
        
        public void Unfocus()
        {
            CloseHubConnection();
        }
    }
}