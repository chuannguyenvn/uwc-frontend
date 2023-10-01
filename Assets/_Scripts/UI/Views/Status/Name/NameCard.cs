using UI.Common;
using UnityEngine.UIElements;

namespace UI.Views.Status.Name
{
    public class NameCard : Card
    {
        private readonly TextElement _roleText;
        private readonly TextElement _nameText;
        
        public NameCard() : base("Name")
        {
            _roleText = new TextElement {name = "Role"};
            _roleText.text = "Driver";
            _roleText.AddToClassList("role");
            _roleText.AddToClassList("sub-text");
            _roleText.AddToClassList("grey-text");
            Add(_roleText);
            
            _nameText = new TextElement {name = "Name"};
            _nameText.text = "Robert Sampletext Jr.";
            _nameText.AddToClassList("name");
            _nameText.AddToClassList("title-text");
            _nameText.AddToClassList("black-text");
            Add(_nameText);
        }
    }
}