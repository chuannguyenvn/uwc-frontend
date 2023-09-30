using UI.Common;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.MCPs
{
    public class McpsView : View
    {
        private McpsDataList _mcpsDataList;
        
        public McpsView() : base ("MCPs")
        {
            _mcpsDataList = new McpsDataList(); 
            Add(_mcpsDataList);
        }
        
        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<McpsView, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}