using UI.Common;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Settings
{
    public class SettingsView : View
    {
        public SettingsView() : base("Settings")
        {
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<SettingsView, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}