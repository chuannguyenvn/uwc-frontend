using Commons.Types;
using UI.Views.Messaging.Contacts;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class TaskList : ScrollView
    {
        public TaskList()
        {
            name = "TaskList";
            for (var i = 0; i < 20; i++)
            {
                var newTask = new TaskEntry();
                
                if (Random.Range(0, 2) == 0)
                {
                    newTask.SetCompletionStatus(true);
                }
                else
                {
                    newTask.SetCompletionStatus(false);
                    newTask.SetFillStatus(Utility.GetRandomEnumValue<McpFillStatus>());
                }
                Add(newTask);
            }
        }
    }
}