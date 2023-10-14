namespace Requests.DataStores.Base
{
    public abstract class ServerSendOnFocusedDataStore<T> : HubBasedDataStore<T>
    {
        public override void Focus()
        {
            SendRequest();
            EstablishHubConnection();
        }
        
        public override void Unfocus()
        {
            CloseHubConnection();
        }
    }
}