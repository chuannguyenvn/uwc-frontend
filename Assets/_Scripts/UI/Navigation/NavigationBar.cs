using System.Collections.Generic;
using Settings;
using UI.Base;

namespace UI.Navigation
{
    public class NavigationBar : AdaptiveElement
    {
        private Panel _background;
        private readonly Dictionary<ViewType, NavigationItem> _navigationItemsByViewType = new();

        public NavigationBar() : base(nameof(NavigationBar))
        {
            ConfigureUss(nameof(NavigationBar));

            CreateBackground();
            CreateNavigationItems();
        }

        private void CreateBackground()
        {
            _background = new Panel("Background");
            _background.AddToClassList(Configs.IS_DESKTOP ? "left-bar" : "bottom-bar");
            Add(_background);
        }

        private void CreateNavigationItems()
        {
            var viewTypes = Configs.IS_DESKTOP ? Configs.DesktopViewTypes : Configs.MobileViewTypes;
            foreach (var viewType in viewTypes)
            {
                var navigationItem = new NavigationItem(viewType);
                _navigationItemsByViewType.Add(viewType, navigationItem);
                Add(navigationItem);
            }
        }

        public void ActivateView(ViewType viewType)
        {
            foreach (var navigationItem in _navigationItemsByViewType)
            {
                navigationItem.Value.RemoveFromClassList("active");
            }

            _navigationItemsByViewType[viewType].AddToClassList("active");
        }
    }
}