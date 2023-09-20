using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.MCPs
{
    public class McpsScreen : VisualElement
    {
        private McpsDataList _mcpsDataList;
        
        public McpsScreen()
        {
            name = "MCPsScreen";
            
            _mcpsDataList = new McpsDataList(); 
            Add(_mcpsDataList);
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