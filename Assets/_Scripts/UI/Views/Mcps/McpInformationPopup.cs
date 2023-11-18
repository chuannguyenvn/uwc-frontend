using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Mcps
{
    public class McpInformationPopup : FullscreenPopup
    {
        public McpInformationPopup()
        {
            ConfigureUss(nameof(McpInformationPopup));

            AddContent(new TextElement() { text = "MCP Information Popup" });
            AddContent(new TextElement() { text = "MCP Information Popup2" });
        }
    }
}