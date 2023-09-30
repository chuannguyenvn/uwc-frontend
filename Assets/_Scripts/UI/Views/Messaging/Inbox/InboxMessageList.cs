using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InboxMessageList : ScrollView
    {
        public InboxMessageList()
        {
            name = "InboxMessageList";

            foreach (var (content, timestamp, isFromUser) in DataStoreDummy.MessageData) Add(new InboxMessageEntry(content, timestamp, isFromUser));
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<InboxMessageList, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : ScrollView.UxmlTraits
        {
        }

        #endregion
    }
}