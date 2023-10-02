using System.Collections.Generic;
using Constants;
using UI.Navigation;
using UI.Views.Messaging;
using UI.Views.Workers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class Root : VisualElement
    {
        public NavigationBar NavigationBar;
        public Dictionary<ViewType, View> ViewsByViewType = new();
        
        public Root()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Common"));
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Base/Root"));
            
            NavigationBar = new NavigationBar();
            Add(NavigationBar);
            
            AddViews();
        }
        
        private void AddViews()
        {
            if (Configs.IS_DESKTOP)
            {
                // ViewsByViewType.Add(ViewType.Workers, new WorkersView());
                ViewsByViewType.Add(ViewType.Messaging, new MessagingView());
            }
            else
            {
                
            }

            foreach (var (viewType, view) in ViewsByViewType)
            {
                Add(view);
            }
        }
        
        public new class UxmlFactory : UxmlFactory<Root, UxmlTraits>
        {
        }
    }
}