using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Settings
{
    public class SettingsScreen : VisualElement
    {
        public SettingsScreen()
        {
            name = "SettingsScreen";
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<SettingsScreen, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}