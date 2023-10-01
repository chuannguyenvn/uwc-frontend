using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InboxMessageList : ScrollView
    {
        public InboxMessageList()
        {
            name = "InboxMessageList";

            foreach (var (content, timestamp, isFromUser) in DataStoreDummy.MessageData)
            {
                var entry = new InboxMessageEntry(content, timestamp, isFromUser);
                Add(entry);
            }

            verticalScroller.value = verticalScroller.highValue > 0 ? verticalScroller.highValue : 0;
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