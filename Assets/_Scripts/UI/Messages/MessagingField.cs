using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Messages
{
    public class MessagingField : TextField
    {
        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<MessagingField, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : TextField.UxmlTraits
        {
        }

        #endregion

        public MessagingField()
        {
            name = "MessagingField";
        }
    }
}