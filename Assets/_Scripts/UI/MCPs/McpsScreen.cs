using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.MCPs
{
    public class McpsScreen : VisualElement
    {
        private McpsList _mcpsList;
        
        public McpsScreen()
        {
            name = "MCPsScreen";
            
            _mcpsList = new McpsList(); 
            Add(_mcpsList);
        }
        
        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<McpsScreen, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}