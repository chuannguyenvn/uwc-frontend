using UnityEngine.UIElements;

namespace UI.Commons
{
    public abstract class List : ScrollView
    {
        public List()
        {
            name = "List";
            AddToClassList("list");
        }
    }
}