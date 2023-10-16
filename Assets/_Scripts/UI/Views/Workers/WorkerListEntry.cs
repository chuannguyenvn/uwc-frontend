using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkerListEntry : AdaptiveElement
    {
        private Image _image;

        private VisualElement _textContainer;
        private TextElement _nameText;
        private TextElement _statusText;

        public WorkerListEntry(Account account) : base(nameof(WorkerListEntry))
        {
            AddToClassList("list-entry");

            _image = new Image { name = "Avatar" };
            Add(_image);

            _textContainer = new VisualElement { name = "TextContainer" };
            Add(_textContainer);

            _nameText = new TextElement { name = "NameText" };
            _nameText.AddToClassList("normal-text");
            _nameText.AddToClassList("black-text");
            _nameText.text = account.FirstName + " " + account.LastName;
            _textContainer.Add(_nameText);

            _statusText = new TextElement { name = "StatusText" };
            _statusText.AddToClassList("sub-text");
            _statusText.AddToClassList("grey-text");
            _statusText.text = account.Address;
            _textContainer.Add(_statusText);
        }
    }
}