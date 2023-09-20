using UnityEngine.UIElements;

namespace UI.Commons
{
    public abstract class DataList : ScrollView
    {
        public DataList()
        {
            name = "List";
            AddToClassList("list");
        }
    }
}