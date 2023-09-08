using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages.Contacts
{
    public class ContactEntry : VisualElement
    {
        private readonly Image _avatar;
        
        private readonly VisualElement _textContainer;
        private readonly TextElement _name;
        private readonly TextElement _messagePreview;

        public ContactEntry()
        {
            name = "ContactEntry";
            AddToClassList("contact-entry");
            AddToClassList("list-item");
            
            _avatar = new Image { name = "Avatar" };
            _avatar.AddToClassList("icon");
            Add(_avatar);
            
            _textContainer = new VisualElement { name = "TextContainer" };
            Add(_textContainer);

            _name = new TextElement { name = "Name" };
            _name.AddToClassList("primary-text");
            _name.text = "Placeholder Name";
            _textContainer.Add(_name);

            _messagePreview = new TextElement { name = "MessagePreview" };
            _messagePreview.AddToClassList("secondary-text");
            _messagePreview.text = "Placeholder Message Preview";
            _textContainer.Add(_messagePreview);
        }

        public ContactEntry(Sprite sprite) : this()
        {
            _avatar.sprite = sprite;
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