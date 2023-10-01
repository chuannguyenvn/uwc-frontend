using Constants;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Common
{
    public abstract class ResponsiveVisualElement : VisualElement
    {
        public ResponsiveVisualElement(string name)
        {
            this.name = name;
            LoadStylesheet(name);
        }
        
        public void LoadStylesheet(string name)
        {
            var commonStylesheet = Resources.Load<StyleSheet>("Stylesheets/Common/" + name);
            if (commonStylesheet == null)
            {
                Debug.LogWarning("Stylesheet not found: Stylesheets/Common/" + name + ".");
                return;
            }
            styleSheets.Add(commonStylesheet);
            
            if (Configs.IS_DESKTOP)
            {
                var desktopStylesheet = Resources.Load<StyleSheet>("Stylesheets/Desktop/" + name);
                if (desktopStylesheet == null)
                {
                    Debug.LogWarning("Stylesheet not found: Stylesheets/Desktop/" + name + ".");
                    return;
                }
                styleSheets.Add(desktopStylesheet);
            }
            else
            {
                var mobileStylesheet = Resources.Load<StyleSheet>("Stylesheets/Mobile/" + name);
                if (mobileStylesheet == null)
                {
                    Debug.LogWarning("Stylesheet not found: Stylesheets/Mobile/" + name + ".");
                    return;
                }
                styleSheets.Add(mobileStylesheet);
            }
        }
    }
}