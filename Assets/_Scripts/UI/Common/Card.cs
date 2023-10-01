namespace UI.Common
{
    public class Card : ResponsiveVisualElement
    {
        public Card(string name) : base(name)
        {
            AddToClassList("card");
        }
    }
}