using UnityEngine.UIElements;

namespace UI.Common
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