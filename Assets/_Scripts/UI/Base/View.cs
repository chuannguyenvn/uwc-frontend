using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public abstract class View : AdaptiveElement
    {
        protected View(string name) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Base/View"));
            AddToClassList("view");
        }

        public virtual void FocusView()
        {
        }

        public virtual void UnfocusView()
        {
        }
    }
}