using Constants;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InboxTitleBar : VisualElement
    {
        private readonly VisualElement _backButton;
        
        private readonly Image _avatar;
        
        private readonly VisualElement _textContainer;
        private readonly TextElement _name;
        private readonly TextElement _status;

        private readonly VisualElement _quickActionsContainer;
        private readonly VisualElement _assignTaskButton;
        private readonly VisualElement _infoButton;

        public InboxTitleBar()
        {
            name = "InboxTitleBar";

            if (Configs.IS_DESKTOP)
            {
                AddToClassList("white-background");
            }
            else
            {
                AddToClassList("colored-background");
                
                _backButton = new VisualElement { name = "BackButton" };
                _backButton.AddToClassList("icon");
                Add(_backButton);
            }
            
            _avatar = new Image { name = "Avatar" };
            Add(_avatar);
            
            _textContainer = new VisualElement { name = "TextContainer" };
            Add(_textContainer);

            _name = new TextElement { name = "Name" };
            _name.text = "Placeholder Name";
            _name.AddToClassList("primary-text");
            _textContainer.Add(_name);

            _status = new TextElement { name = "Status" };
            _status.text = "Placeholder Status";
            _status.AddToClassList("secondary-text");
            _textContainer.Add(_status);

            if (Configs.IS_DESKTOP)
            {
                _quickActionsContainer = new VisualElement { name = "QuickActionsContainer" };
                _quickActionsContainer.AddToClassList("colored-background");
                Add(_quickActionsContainer);

                _assignTaskButton = new VisualElement { name = "AssignTaskButton" };
                _assignTaskButton.AddToClassList("icon");
                _quickActionsContainer.Add(_assignTaskButton);

                _infoButton = new VisualElement { name = "InfoButton" };
                _infoButton.AddToClassList("icon");
                _quickActionsContainer.Add(_infoButton);
            }
            else
            {
                _name.AddToClassList("white-text");
                _status.AddToClassList("white-text");

            }
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<InboxTitleBar, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}