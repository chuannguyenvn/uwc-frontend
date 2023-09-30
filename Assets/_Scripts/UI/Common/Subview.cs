namespace UI.Common
{
    public class Subview : ResponsiveVisualElement
    {
        public Subview(string name) : base(name)
        {
            AddToClassList("subview");
        }
    }
}