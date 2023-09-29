using Constants;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Common
{
    public abstract class ResponsiveVisualElement : VisualElement
    {
        public ResponsiveVisualElement(string name)
        {
            var commonStylesheet = Resources.Load<StyleSheet>("Stylesheets/Common/" + name);
            if (commonStylesheet == null) return;
            styleSheets.Add(commonStylesheet);
            
            if (Debugs.IS_DESKTOP)
            {
                var desktopStylesheet = Resources.Load<StyleSheet>("Stylesheets/Desktop/" + name);
                if (desktopStylesheet == null) return;
                styleSheets.Add(desktopStylesheet);
            }
            else
            {
                var mobileStylesheet = Resources.Load<StyleSheet>("Stylesheets/Mobile/" + name);
                if (mobileStylesheet == null) return;
                styleSheets.Add(mobileStylesheet);
            }
        }
    }
}