using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages
{
    public class ContactsList : ScrollView
    {
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

        public ContactsList()
        {
            name = "ContactsList";
            for (int i = 0; i < 20; i++)
            {
                Add(new ContactEntry());
            }
        }
    }
}