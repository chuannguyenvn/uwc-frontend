using UI.Commons;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.MCPs
{
    public class McpsDataList : DataList
    {
        public McpsDataList()
        {
            name = "McpsList";

            if (Configs.IS_DEBUGGING)
                for (var i = 0; i < 20; i++)
                    Add(new McpEntry());
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<McpsDataList, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : DataList.UxmlTraits
        {
        }

        #endregion
    }
}