using UnityEngine.UIElements;

namespace UI.Common
{
    public class Subview : ResponsiveVisualElement
    {
        public Subview(string name) : base(name)
        {
            AddToClassList("subview");
            AddToClassList("subview-" + name.ToLower());
        }
    }
}