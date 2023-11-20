using System.Collections.Generic;
using Authentication;
using Commons.Communications.Messages;
using Requests;
using Settings;
using UI.Base;
using UI.Reusables;
using UI.Reusables.Control;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Messaging.Contacts
{
    public class ContactList : View
    {
        private ListControl _listControl;
        private ScrollViewWithShadow _scrollView;

        private List<ContactListEntry> _workerListEntries = new();

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
            _workerListEntries.Clear();
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

                _workerListEntries.Add(entry);
                _scrollView.AddToScrollView(entry);
                if (i == 0) firstEntry = entry;
            }

            if (firstEntry != null)
            {
                firstEntry.ShowMessages();
            }
        }

        private void SearchHandler(string text)
        {
            text = Utility.CreateSearchString(text);
            foreach (var entry in _workerListEntries)
            {
                if (Utility.CreateSearchString(entry.ContactName, entry.PreviewMessage).Contains(text) ||
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