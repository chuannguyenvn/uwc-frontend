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

            makeItem = HandleMakeItem;
            bindItem = HandleBindItem;
            itemsSource = DataStoreDummy.MessageData;
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