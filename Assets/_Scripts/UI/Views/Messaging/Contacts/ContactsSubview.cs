using UI.Common;

namespace UI.Views.Messaging.Contacts
{
    public class ContactsSubview : Subview
    {
        private readonly ContactList _contactList;

        public ContactsSubview() : base("Contacts")
        {
            _contactList = new ContactList();
            Add(_contactList);
        }
    }
}