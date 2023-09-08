using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Messages
{
    public class InboxMessageList : ScrollView
    {
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

        public InboxMessageList()
        {
            name = "InboxMessageList";

            foreach (var (content, timestamp, isFromUser) in DataStoreDummy.MessageData)
            {
                Add(new InboxMessageEntry(content, timestamp, isFromUser));
            }
        }

        private VisualElement HandleMakeItem()
        {
            return new InboxMessageEntry();
        }

        private void HandleBindItem(VisualElement item, int index)
        {
            var messageEntry = item as InboxMessageEntry;
            var data = DataStoreDummy.MessageData[index];
            messageEntry?.SetData(data.content, data.timestamp, data.isFromUser);
        }
    }
}