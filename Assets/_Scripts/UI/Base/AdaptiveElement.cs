using Settings;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Base
{
    public class AdaptiveElement : VisualElement
    {
        protected AdaptiveElement(string name)
        {
            this.name = name;
            styleSheets.AddByName(nameof(AdaptiveElement));
            AddToClassList(Configs.IS_DESKTOP ? "desktop" : "mobile");

            RegisterCallback<MouseOverEvent>(evt => { Root.IsMouseOverElement = true; });
            RegisterCallback<MouseOutEvent>(evt => { Root.IsMouseOverElement = false; });
            RegisterCallback<MouseDownEvent>(evt => { Root.IsMouseDownElement = true; });
            RegisterCallback<MouseUpEvent>(evt => { Root.IsMouseDownElement = false; });
        }

        protected void ConfigureUss(string ussName)
        {
            styleSheets.AddByName(ussName);
            AddToClassList(ussName.PascalToKebabCase());
        }
    }
}