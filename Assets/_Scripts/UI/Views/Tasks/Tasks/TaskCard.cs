using UI.Base;

namespace UI.Views.Tasks.Tasks
{
    public class TaskCard : AdaptiveElement
    {
        protected TaskCard(string name) : base(name)
        {
            ConfigureUss(nameof(TaskCard));
        }
    }
}