using Commons.Models;
using Maps;
using UI.Base;
using UI.Views.Mcps.AssignTaskProcedure;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;

namespace UI.Views.Workers
{
    public class WorkerListEntry : AnimatedButton
    {
        public UserProfile Profile { get; private set; }
        private readonly bool _isTaskAssigning;

        private TextElement _avatar;

        private VisualElement _textContainer;
        private TextElement _nameText;
        private TextElement _statusText;

        public WorkerListEntry(UserProfile profile, bool isTaskAssigning) : base(nameof(WorkerListEntry))
        {
            Profile = profile;
            _isTaskAssigning = isTaskAssigning;
            if (_isTaskAssigning) AddToClassList("task-assigning");

            ConfigureUss(nameof(WorkerListEntry));

            AddToClassList("white-button");
            AddToClassList("iconless-button");
            AddToClassList("rounded-button-16px");

            CreateImage(profile);
            CreateDetails(profile);

            if (_isTaskAssigning)
            {
                AddToClassList("task-assigning");
                Clicked += TaskAssigningWorkerClickedHandler;
                ChooseWorkerStep.WorkerIdChanged += RefreshAssigningStatus;
            }
            else
            {
                Clicked += () => MapManager.Instance.ZoomToWorker(profile.Id);
            }
        }

        private void CreateImage(UserProfile profile)
        {
            _avatar = new TextElement { name = "Avatar" };
            _avatar.AddToClassList("white-text");
            _avatar.AddToClassList("title-text");
            _avatar.text = profile.FirstName[0].ToString();
            _avatar.style.backgroundColor = Color.HSVToRGB(profile.AvatarColorHue / 360f, 0.7f, 0.8f);
            Add(_avatar);
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

        private void TaskAssigningWorkerClickedHandler()
        {
            if (ChooseWorkerStep.WorkerId != Profile.Id) ChooseWorkerStep.SetWorkerId(Profile.Id);
            else ChooseWorkerStep.SetWorkerId(-1);

            RefreshAssigningStatus();
        }

        public void RefreshAssigningStatus()
        {
            EnableInClassList("chosen", ChooseWorkerStep.WorkerId == Profile.Id);
        }
    }
}