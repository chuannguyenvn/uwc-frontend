using System;
using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkerListEntry : AnimatedButton
    {
        public UserProfile Profile { get; }

        private Image _image;

        private VisualElement _textContainer;
        private TextElement _nameText;
        private TextElement _statusText;

        public WorkerListEntry(UserProfile profile) : base(nameof(WorkerListEntry))
        {
            Profile = profile;

            ConfigureUss(nameof(WorkerListEntry));

            AddToClassList("white-button");
            AddToClassList("iconless-button");
            AddToClassList("rounded-button-16px");

            // CreateImage();
            CreateDetails(profile);
        }

        private void CreateImage()
        {
            _image = new Image { name = "Avatar" };
            Add(_image);
        }

        private void CreateDetails(UserProfile profile)
        {
            _textContainer = new VisualElement { name = "TextContainer" };
            Add(_textContainer);

            _nameText = new TextElement { name = "NameText" };
            _nameText.AddToClassList("normal-text");
            _nameText.AddToClassList("black-text");
            _nameText.text = profile.FirstName + " " + profile.LastName;
            _textContainer.Add(_nameText);

            _statusText = new TextElement { name = "StatusText" };
            _statusText.AddToClassList("sub-text");
            _statusText.AddToClassList("grey-text");
            _statusText.text = profile.Address;
            _textContainer.Add(_statusText);
        }
    }
}