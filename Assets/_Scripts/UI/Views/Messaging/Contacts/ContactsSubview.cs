using Constants;
using UI.Common;
using UnityEngine;

namespace UI.Views.Messaging.Contacts
{
    public class ContactsSubview : Subview
    {
        private readonly ContactList _contactList;

        public ContactsSubview() : base("Contacts")
        {
            if (!Configs.IS_DESKTOP) AddToClassList("full-screen-view");
            _contactList = new ContactList();
            Add(_contactList);
        }
    }
}