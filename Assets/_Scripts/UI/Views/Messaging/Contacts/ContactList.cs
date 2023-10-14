﻿using System.Collections.Generic;
using Commons.Communications.Messages;
using Commons.Models;
using Constants;
using Managers;
using Newtonsoft.Json;
using Requests;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Contacts
{
    public class ContactList : View
    {
        private ScrollView _scrollView;

        public ContactList() : base(nameof(ContactList))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Contacts/ContactList"));
            AddToClassList(Configs.IS_DESKTOP ? "side-view" : "full-view");

            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);

            DataStoreManager.Messaging.ContactList.DataUpdated += DataUpdatedHandler;
        }

        ~ContactList()
        {
            DataStoreManager.Messaging.ContactList.DataUpdated -= DataUpdatedHandler;
        }

        private void DataUpdatedHandler(GetPreviewMessagesResponse data)
        {
            _scrollView.Clear();

            for (int i = 0; i < data.FullNames.Count; i++)
            {
                _scrollView.Add(new ContactListEntry(
                    data.FullNames[i],
                    data.Messages[i].Content,
                    data.Messages[i].Timestamp,
                    data.Messages[i].SenderAccountId == AuthenticationManager.Instance.UserAccountId));
            }
        }
    }
}