using System;
using LocalizationNS;
using Settings;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Navigation
{
    public class NavigationItem : AdaptiveElement
    {
        private VisualElement _icon;
        private Label _label;

        public NavigationItem(ViewType viewType) : base(nameof(NavigationItem))
        {
            ConfigureUss(nameof(NavigationItem));

            name = viewType.ToString();

            CreateIcon();
            CreateLabel(viewType);

            RegisterCallback<MouseUpEvent>(_ => { GetFirstAncestorOfType<Root>().ActivateView(viewType); });
        }

        private void CreateIcon()
        {
            _icon = new VisualElement { name = "Icon" };
            _icon.AddToClassList("icon");
            Add(_icon);
        }

        private void CreateLabel(ViewType viewType)
        {
            _label = new Label { name = "Label" };
            _label.AddToClassList("sub-text");
            _label.AddToClassList("white-text");
            var sentence = viewType switch
            {
                ViewType.Map => Sentence.MapView.MAP,
                ViewType.Workers => Sentence.WorkersView.WORKERS,
                ViewType.Mcps => Sentence.McpsView.MCPS,
                ViewType.Vehicles => Sentence.VehiclesView.VEHICLES,
                ViewType.Tasks => Sentence.TasksView.TASKS,
                ViewType.Status => Sentence.StatusView.STATUS,
                ViewType.Reporting => Sentence.ReportingView.REPORTING,
                ViewType.Messaging => Sentence.MessagingView.MESSAGING,
                ViewType.Settings => Sentence.SettingsView.SETTINGS,
                _ => throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null)
            };
            _label.text = Localization.GetSentence(sentence);
            Add(_label);
        }
    }
}