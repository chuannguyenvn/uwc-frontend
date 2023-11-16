using Settings;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class AdaptiveElement : VisualElement
    {
        protected AdaptiveElement(string name)
        {
            this.name = name;
            AddToClassList(Configs.IS_DESKTOP ? "desktop" : "mobile");

            RegisterCallback<MouseOverEvent>(evt =>
            {
                Root.IsMouseOverElement = true;
            });

            RegisterCallback<MouseOutEvent>(evt =>
            {
                Root.IsMouseOverElement = false;
            });

            RegisterCallback<MouseDownEvent>(evt =>
            {
                Root.IsMouseDownElement = true;
            });

            RegisterCallback<MouseUpEvent>(evt =>
            {
                Root.IsMouseDownElement = false;
            });
        }
    }
}