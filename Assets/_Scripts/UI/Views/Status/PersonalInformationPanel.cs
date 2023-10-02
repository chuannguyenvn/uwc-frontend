using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Status
{
    public class PersonalInformationPanel : Panel
    {
        public TextElement RoleText;
        public TextElement NameText;

        public PersonalInformationPanel() : base(nameof(PersonalInformationPanel))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Status/PersonalInformationPanel"));
            AddToClassList("rounded-32px");

            RoleText = new TextElement { name = "RoleText" };
            RoleText.AddToClassList("sub-text");
            RoleText.AddToClassList("grey-text");
            RoleText.text = "Driver";
            Add(RoleText);

            NameText = new TextElement { name = "NameText" };
            NameText.AddToClassList("normal-text");
            NameText.AddToClassList("black-text");
            NameText.text = "Placeholder Name";
            Add(NameText);
        }
    }
}