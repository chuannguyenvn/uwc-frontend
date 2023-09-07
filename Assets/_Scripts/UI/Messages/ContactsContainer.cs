using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages
{
    public class ContactsContainer : VisualElement
    {
        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<ContactsContainer, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion

        public ContactsContainer()
        {
            name = "ContactsContainer";
            for (int i = 0; i < 20; i++)
            {
                Add(new ContactEntry());
            }
        }
    }
}