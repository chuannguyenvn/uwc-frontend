using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages.Contacts
{
    public class ContactsList : ScrollView
    {
        public ContactsList()
        {
            name = "ContactsList";
            AddToClassList("list");
            for (var i = 0; i < 20; i++) Add(new ContactEntry());
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<ContactsList, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : ScrollView.UxmlTraits
        {
        }

        #endregion
    }
}