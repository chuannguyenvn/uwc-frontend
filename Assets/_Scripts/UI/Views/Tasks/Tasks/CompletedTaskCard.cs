using Commons.Models;
using Commons.Types;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class CompletedTaskCard : TaskCard
    {
        private VisualElement _contentContainer;
        private TextElement _addressText;

        public CompletedTaskCard(TaskData taskData) : base(nameof(CompletedTaskCard))
        {
            ConfigureUss(nameof(CompletedTaskCard));

            _contentContainer = new VisualElement { name = "ContentContainer" };
            Add(_contentContainer);

            _addressText = new TextElement { name = "AddressText" };
            _addressText.text = $"<i><s>{taskData.McpData.Address}</s></i>";
            _addressText.AddToClassList("normal-text");
            _addressText.AddToClassList(taskData.TaskStatus switch
            {
                TaskStatus.Completed => "green-text",
                TaskStatus.Rejected => "red-text",
                _ => "grey-text"
            });
            _contentContainer.Add(_addressText);
        }
    }
}