using UI.Common;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.MCPs
{
    public class McpEntry : DataListEntry
    {
        public McpEntry()
        {
            name = "McpEntry";

            Icon.name = "McpIcon";
            PrimaryText.name = "Address";
            SecondaryText.name = "FillPercentage";

            SetData("Placeholder Address", 0.0f);
        }

        public McpEntry(string address, float fillPercentage) : this()
        {
            SetData(address, fillPercentage);
        }

        public void SetData(string address, float fillPercentage)
        {
            PrimaryText.text = address;
            SecondaryText.text = "Current load: " + fillPercentage.ToString("F2");
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<McpEntry, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}