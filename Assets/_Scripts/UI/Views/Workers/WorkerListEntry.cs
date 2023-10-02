using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkerListEntry : AdaptiveElement
    {
        private Image _image;
        private TextElement _nameText;
        private TextElement _statusText;

        public WorkerListEntry(UserProfile workerProfile) : base(nameof(WorkerListEntry))
        {
            _image = new Image { name = "Image" };
            Add(_image);

            _nameText = new TextElement { name = "PrimaryText" };
            _nameText.AddToClassList("normal-text");
            _nameText.AddToClassList("black-text");
            _nameText.text = workerProfile.FirstName + " " + workerProfile.LastName;
            Add(_nameText);

            _statusText = new TextElement { name = "SecondaryText" };
            _nameText.AddToClassList("sub-text");
            _nameText.AddToClassList("grey-text");
            _statusText.text = workerProfile.Address;
            Add(_statusText);
        }
    }
}