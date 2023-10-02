using Commons.Models;
using Constants;
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

            for (int i = 0; i < 30; i++)
            {
                _scrollView.Add(new ContactListEntry(new Message()));
            }
        }
    }
}