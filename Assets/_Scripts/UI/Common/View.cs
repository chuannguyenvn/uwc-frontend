namespace UI.Common
{
    public abstract class View : ResponsiveVisualElement
    {
        public View(string name) : base(name)
        {
            AddToClassList("white-background");
        }
    }
}