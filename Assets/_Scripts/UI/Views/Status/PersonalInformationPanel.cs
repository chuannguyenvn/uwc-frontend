﻿using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Status
{
    public class PersonalInformationPanel : Panel
    {
        private TextElement _roleText;
        private TextElement _nameText;

        public PersonalInformationPanel() : base(nameof(PersonalInformationPanel))
        {
            ConfigureUss(nameof(PersonalInformationPanel));

            AddToClassList("rounded-32px");

            CreateRoleText();
            CreateNameText();
        }

        private void CreateRoleText()
        {
            _roleText = new TextElement { name = "RoleText" };
            _roleText.AddToClassList("sub-text");
            _roleText.AddToClassList("grey-text");
            _roleText.text = "Driver";
            Add(_roleText);
        }

        private void CreateNameText()
        {
            _nameText = new TextElement { name = "NameText" };
            _nameText.AddToClassList("normal-text");
            _nameText.AddToClassList("black-text");
            _nameText.text = "Placeholder Name";
            Add(_nameText);
        }
    }
}