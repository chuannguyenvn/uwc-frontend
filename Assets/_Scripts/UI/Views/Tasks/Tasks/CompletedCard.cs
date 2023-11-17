using Commons.Models;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class CompletedCard : View
    {
        private VisualElement _contentContainer;
        private TextElement _addressText;
        private VisualElement _crossLine;

        public CompletedCard(TaskData taskData) : base(nameof(CompletedCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Tasks/CompletedCard"));
            AddToClassList("task-card");

            _contentContainer = new VisualElement { name = "ContentContainer" };
            Add(_contentContainer);

            _addressText = new TextElement { name = "AddressText" };
            _addressText.text = taskData.McpData.Address;
            _addressText.AddToClassList("normal-text");
            _addressText.AddToClassList("grey-text");
            _contentContainer.Add(_addressText);
            
            _crossLine = new VisualElement { name = "CrossLine" };
            _contentContainer.Add(_crossLine);
        }
    }
}