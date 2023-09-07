using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages
{
    public class InboxContainer : VisualElement
    {
        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<InboxContainer, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion

        private VisualElement _titleContainer;
        private Image _avatar;
        private TextElement _name;
        private TextElement _status;
        
        public InboxContainer()
        {
            name = "InboxContainer";
            
            _titleContainer = new VisualElement { name = "TitleContainer" };
            Add(_titleContainer);
            
            _avatar = new Image { name = "Avatar" };
            _titleContainer.Add(_avatar);
            
            _name = new TextElement { name = "Name" };
            _name.AddToClassList("primary-text");
            _titleContainer.Add(_name);
            
            _status = new TextElement { name = "Status" };
            _status.AddToClassList("secondary-text");
            _titleContainer.Add(_status);
        }
    }
}