using UnityEngine.UIElements;

namespace UI.Common
{
    public class FullScreenView : View
    {
        public FullScreenView(string name) : base(name)
        {
            this.name = name;
            AddToClassList("full-screen-view");
        }
    }
}