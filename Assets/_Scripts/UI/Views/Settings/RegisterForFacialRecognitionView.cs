using System.Threading.Tasks;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class RegisterForFacialRecognitionView : View
    {
        private Panel _guidePanel;
        private AnimatedButton _backButton;
        private VisualElement _cameraView;
        private VisualElement _faceRegIcon;
        private TextElement _guideText;
        private AnimatedButton _startButton;

        public RegisterForFacialRecognitionView() : base(nameof(RegisterForFacialRecognitionView))
        {
            ConfigureUss(nameof(RegisterForFacialRecognitionView));

            CreateCameraView();
            CreateGuide();
            CreateBackButton();

            BringToFront();
        }

        private void CreateCameraView()
        {
            _cameraView = new VisualElement { name = "CameraView" };
            Add(_cameraView);
        }

        private void CreateGuide()
        {
            _guidePanel = new Panel { name = "GuidePanel" };
            _guidePanel.AddToClassList("rounded-64px");
            Add(_guidePanel);

            _faceRegIcon = new VisualElement { name = "FaceRegIcon" };
            _guidePanel.Add(_faceRegIcon);

            _guideText = new TextElement { name = "GuideText" };
            _guideText.AddToClassList("white-text");
            _guideText.AddToClassList("normal-text");
            _guideText.text = "Please look at the camera and press the button below to start.";
            _guidePanel.Add(_guideText);

            _startButton = new AnimatedButton { name = "StartButton" };
            _startButton.AddToClassList("iconless-button");
            _startButton.AddToClassList("green-button");
            _startButton.AddToClassList("rounded-button-64px");
            _startButton.SetText("Start");
            _startButton.AddToTextClassList("title-text");
            _startButton.AddToTextClassList("white-text");
            _startButton.Clicked += () => RootController.Instance.StartTakingPhotos(
                () =>
                {
                    _guideText.text = "Please keep your head still...";
                    _startButton.style.display = DisplayStyle.None;
                },
                async () =>
                {
                    _faceRegIcon.AddToClassList("done");
                    _guideText.text = "Done!";
                    await Task.Delay(2000);
                    Hide();
                });
            Add(_startButton);
        }

        private void CreateBackButton()
        {
            _backButton = new AnimatedButton { name = "BackButton" };
            _backButton.Clicked += Hide;
            _guidePanel.Add(_backButton);
        }

        public void Show()
        {
            style.display = DisplayStyle.Flex;
            if (RootController.Instance) RootController.Instance.ShowCamera(_cameraView);
        }

        public void Hide()
        {
            style.display = DisplayStyle.None;
            if (RootController.Instance) RootController.Instance.HideCamera();
        }
    }
}