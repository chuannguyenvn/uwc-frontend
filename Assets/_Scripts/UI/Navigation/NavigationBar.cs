using System.Collections.Generic;
using Constants;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Navigation
{
    public class NavigationBar : AdaptiveElement
    {
        private readonly Panel _background;
        public Dictionary<ViewType, NavigationItem> NavigationItemsByViewType = new();

        public NavigationBar() : base(nameof(NavigationBar))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Navigation/NavigationBar"));
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Navigation/NavigationItem"));
            AddToClassList("navigation-bar");

            _background = new Panel("Background");
            _background.AddToClassList(Configs.IS_DESKTOP ? "left-bar" : "bottom-bar");
            Add(_background);

            AddNavigationItems();
        }

        private void AddNavigationItems()
        {
            if (Configs.IS_DESKTOP)
            {
                foreach (var viewType in Configs.DesktopViewTypes)
                {
                    var navigationItem = new NavigationItem(viewType);
                    NavigationItemsByViewType.Add(viewType, navigationItem);
                    Add(navigationItem);
                }
            }
            else
            {
                foreach (var viewType in Configs.MobileViewTypes)
                {
                    var navigationItem = new NavigationItem(viewType);
                    NavigationItemsByViewType.Add(viewType, navigationItem);
                    Add(navigationItem);
                }
            }
        }
        
        public void ActivateView(ViewType viewType)
        {
            foreach (var navigationItem in NavigationItemsByViewType)
            {
                navigationItem.Value.RemoveFromClassList("active");
            }
            
            NavigationItemsByViewType[viewType].AddToClassList("active");
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<NavigationBar, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}