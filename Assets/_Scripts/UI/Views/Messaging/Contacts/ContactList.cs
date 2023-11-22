using System.Collections.Generic;
using Authentication;
using Commons.Communications.Messages;
using Requests;
using Settings;
using UI.Base;
using UI.Reusables;
using UI.Reusables.Control;
using UI.Views.Messaging.Inbox;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Messaging.Contacts
{
    public class ContactList : View
    {
        private ListControl _listControl;
        private ScrollViewWithShadow _scrollView;

        private List<ContactListEntry> _contactListEntries = new();

        public ContactList() : base(nameof(ContactList))
        {
            ConfigureUss(nameof(ContactList));

            if (!Configs.IS_DESKTOP) AddToClassList("full-view");

            CreateControls();
            CreateScrollView();

            DataStoreManager.Messaging.ContactList.DataUpdated += DataUpdatedHandler;
        }

        ~ContactList()
        {
            DataStoreManager.Messaging.ContactList.DataUpdated -= DataUpdatedHandler;
        }

        private void CreateControls()
        {
            _listControl = new ListControl(SearchHandler);
            Add(_listControl);
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollViewWithShadow(ShadowType.InnerTop) { name = "ScrollView" };
            Add(_scrollView);
        }

        private void DataUpdatedHandler(GetPreviewMessagesResponse data)
        {
            _scrollView.Clear();
            _contactListEntries.Clear();
            for (int i = 0; i < data.FullNames.Count; i++)
            {
                var otherUserId = data.Messages[i].SenderAccountId == AuthenticationManager.Instance.UserAccountId
                    ? data.Messages[i].ReceiverAccountId
                    : data.Messages[i].SenderAccountId;

                var entry = new ContactListEntry(otherUserId,
                    data.FullNames[i],
                    data.Messages[i].Content,
                    data.Messages[i].Timestamp,
                    data.Messages[i].SenderAccountId == AuthenticationManager.Instance.UserAccountId);

                _contactListEntries.Add(entry);
                _scrollView.AddToScrollView(entry);
            }

            if (data.FullNames.Count > 0)
            {
                GetFirstAncestorOfType<MessagingView>().Q<InboxContainer>()
                    .SwitchInbox(_contactListEntries[0].OtherUserId, _contactListEntries[0].FullName);
            }
        }

        private void SearchHandler(string text)
        {
            text = Utility.CreateSearchString(text);
            foreach (var entry in _contactListEntries)
            {
                if (Utility.CreateSearchString(entry.FullName, entry.PreviewMessage).Contains(text) ||
                    text == "")
                {
                    entry.style.display = DisplayStyle.Flex;
                }
                else
                {
                    entry.style.display = DisplayStyle.None;
                }
            }
        }
    }
}