using UnityEngine.UIElements;

namespace UI.Base.ListView
{
    public class ListEntry<T> : AdaptiveElement
    {
        private Image _image;
        private TextElement _primaryText;
        private TextElement _secondaryText;
        
        public ListEntry(string name) : base(name)
        {
            _image = new Image { name = "Image" };
            Add(_image);
            
            _primaryText = new TextElement { name = "PrimaryText" };
            _primaryText.AddToClassList("normal-text");
            _primaryText.AddToClassList("black-text");
            Add(_primaryText);
            
            _secondaryText = new TextElement { name = "SecondaryText" };
            _primaryText.AddToClassList("sub-text");
            _primaryText.AddToClassList("grey-text");
            Add(_secondaryText);
        }
        
        protected void SetPrimaryText(string text)
        {
            _primaryText.text = text;
        }
        
        protected void SetSecondaryText(string text)
        {
            _secondaryText.text = text;
        }
    }
}