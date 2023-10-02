using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class EmptyingLogPanel : Panel
    {
        public EmptyingLogPanel() : base(nameof(EmptyingLogPanel))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Details/EmptyingLogPanel"));
            AddToClassList("rounded-32px");
        }
    }
}