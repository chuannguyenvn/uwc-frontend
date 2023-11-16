using Commons.Models;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class CompletedCard : View
    {
        public VisualElement ContentContainer;
        public TextElement AddressText;
        public VisualElement CrossLine;

        public CompletedCard(TaskData taskData) : base(nameof(CompletedCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Tasks/CompletedCard"));
            AddToClassList("task-card");

            ContentContainer = new VisualElement { name = "ContentContainer" };
            Add(ContentContainer);

            AddressText = new TextElement { name = "AddressText" };
            AddressText.text = taskData.McpData.Address;
            AddressText.AddToClassList("normal-text");
            AddressText.AddToClassList("grey-text");
            ContentContainer.Add(AddressText);
            
            CrossLine = new VisualElement { name = "CrossLine" };
            ContentContainer.Add(CrossLine);
        }
    }
}