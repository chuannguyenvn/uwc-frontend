using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.MCPs
{
    public class McpsList : ScrollView
    {
        public McpsList()
        {
            name = "McpsList";
            AddToClassList("list");
            for (var i = 0; i < 20; i++) Add(new McpEntry());
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<McpsList, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : ScrollView.UxmlTraits
        {
        }

        #endregion
    }
}