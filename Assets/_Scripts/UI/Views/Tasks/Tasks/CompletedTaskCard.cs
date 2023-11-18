using Commons.Models;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class CompletedTaskCard : TaskCard
    {
        private VisualElement _contentContainer;
        private TextElement _addressText;
        private VisualElement _crossLine;

        public CompletedTaskCard(TaskData taskData) : base(nameof(CompletedTaskCard))
        {
            ConfigureUss(nameof(CompletedTaskCard));
            
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