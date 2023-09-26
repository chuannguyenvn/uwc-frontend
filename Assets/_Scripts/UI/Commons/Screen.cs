using Constants;
using UnityEngine.UIElements;

namespace UI.Commons
{
    public class Screen : VisualElement
    {
        public Screen()
        {
            AddToClassList(Debugs.IS_DESKTOP ? "desktop" : "mobile");
        }
    }
}