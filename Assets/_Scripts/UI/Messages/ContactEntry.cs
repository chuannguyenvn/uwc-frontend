using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages
{
    public class ContactEntry : VisualElement
    {
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

        private Image _avatar;

        private VisualElement _textContainer;
        private TextElement _name;
        private TextElement _messagePreview;

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

        public ContactEntry(Sprite sprite)
        {
            name = "ContactEntry";
            AddToClassList("contact-entry");
            AddToClassList("list-item");


            _avatar = new Image { name = "Avatar", sprite = sprite };
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
    }
}