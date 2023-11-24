using UI.Base;

namespace UI.Views.Map
{
    public class NextStopPanel : Panel
    {
        private VisualElement _nextStopContainer;
        private VisualElement _nextStopIcon;
        private TextElement _nextStopTitle;
        
        private TextElement _nextStopAddress;

        public NextStopPanel()
        {
ConfigureUss(nameof(NextStopPanel));

            CreateNextStopContainer();
            CreateNextStopAddress();
        }

        private void CreateNextStopContainer()
        {
            _nextStopContainer = new VisualElement() {name = "NextStopContainer"};
            Add(_nextStopContainer);

            _nextStopIcon = new VisualElement() {name = "NextStopIcon"};
            _nextStopContainer.Add(_nextStopIcon);

            _nextStopTitle = new TextElement() {name = "NextStopTitle"};
            _nextStopTitle.AddToClassList("sub-text");
            _nextStopTitle.AddToClassList("white-text");
            _nextStopContainer.Add(_nextStopTitle);
        }

        private void CreateNextStopAddress()
        {
            _nextStopAddress = new TextElement() {name = "NextStopAddress"};
            _nextStopAddress.AddToClassList("title-text");
            _nextStopAddress.AddToClassList("white-text");
            Add(_nextStopAddress);
        }

        public void SetNextStopAddress(string address)
        {
            _nextStopAddress.text = address;
        }
    }
}