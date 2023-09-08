using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Messages
{
    public class InboxMessageList : ListView
    {
        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<InboxMessageList, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : ListView.UxmlTraits
        {
        }

        #endregion

        public InboxMessageList()
        {
            name = "InboxMessageList";
        }
    }
}