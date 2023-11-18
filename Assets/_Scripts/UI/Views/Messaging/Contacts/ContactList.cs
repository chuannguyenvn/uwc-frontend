using Authentication;
using Commons.Communications.Messages;
using Requests;
using Settings;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Contacts
{
    public class ContactList : View
    {
        private ScrollView _scrollView;

        public ContactList() : base(nameof(ContactList))
        {
            ConfigureUss(nameof(ContactList));

            if (!Configs.IS_DESKTOP) AddToClassList("full-view");

            CreateScrollView();

            DataStoreManager.Messaging.ContactList.DataUpdated += DataUpdatedHandler;
        }

        ~ContactList()
        {
            DataStoreManager.Messaging.ContactList.DataUpdated -= DataUpdatedHandler;
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);
        }

        private void DataUpdatedHandler(GetPreviewMessagesResponse data)
        {
            _scrollView.Clear();
            ContactListEntry firstEntry = null;
            for (int i = 0; i < data.FullNames.Count; i++)
            {
                var entry = new ContactListEntry(
                    data.Messages[i].SenderAccountId == AuthenticationManager.Instance.UserAccountId
                        ? data.Messages[i].ReceiverAccountId
                        : data.Messages[i].SenderAccountId,
                    data.FullNames[i],
                    data.Messages[i].Content,
                    data.Messages[i].Timestamp,
                    data.Messages[i].SenderAccountId == AuthenticationManager.Instance.UserAccountId);

                _scrollView.Add(entry);
                if (i == 0) firstEntry = entry;
            }

            if (firstEntry != null)
            {
                firstEntry.ShowMessages();
            }
        }
    }
}