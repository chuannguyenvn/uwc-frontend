using UI.Commons;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages.Contacts
{
    public class ContactEntry : ListEntry
    {
        public ContactEntry()
        {
            name = "ContactEntry";
            AddToClassList("contact-entry");

            Icon.name = "Avatar";

            PrimaryText.name = "Name";
            PrimaryText.text = "Placeholder Name";

            SecondaryText.name = "MessagePreview";
            SecondaryText.text = "Placeholder Message Preview";
        }

        public ContactEntry(Sprite sprite) : this()
        {
            Icon.sprite = sprite;
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<ContactEntry, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}