using Constants;
using UI.Common;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Contacts
{
    public class ContactEntry : DataListEntry
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

            RegisterCallback<MouseUpEvent>(MouseUpHandler);
        }

        public ContactEntry(Sprite sprite) : this()
        {
            Icon.sprite = sprite;
        }

        private void MouseUpHandler(MouseUpEvent evt)
        {
            if (Configs.IS_DESKTOP)
            {
            }
            else
            {
                MessagingController.Instance.ShowInbox();
            }
        }
    }
}