using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class View : AdaptiveElement
    {
        public View(string name) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Base/View"));

            AddToClassList("view");
        }
    }
}