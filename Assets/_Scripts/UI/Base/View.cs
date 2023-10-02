using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class View : AdaptiveElement
    {
        public View(string name, bool isFullScreen) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Base/View"));

            AddToClassList("view");
            AddToClassList(isFullScreen ? "full-view" : "side-view");
        }
    }
}