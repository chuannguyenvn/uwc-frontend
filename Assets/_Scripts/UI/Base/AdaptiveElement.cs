using Constants;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class AdaptiveElement : VisualElement
    {
        public AdaptiveElement(string name)
        {
            this.name = name;
            AddToClassList(Configs.IS_DESKTOP ? "desktop" : "mobile");
        }
    }
}