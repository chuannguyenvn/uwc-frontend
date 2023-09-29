using Constants;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Common
{
    public abstract class View : ResponsiveVisualElement
    {
        public View(string name) : base(name)
        {
        }
    }
}