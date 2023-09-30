using UnityEngine.UIElements;

namespace UI.Views.Messaging.Contacts
{
    public class ContactList : ScrollView
    {
        public ContactList()
        {
            name = "ContactList";
            for (var i = 0; i < 20; i++) Add(new ContactEntry());
        }
    }
}