using Commons.Communications.Messages;
using Requests;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class MessageList : AdaptiveElement
    {
        private ScrollView ScrollView;

        public MessageList() : base(nameof(MessageList))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Inbox/MessageList"));

            ScrollView = new ScrollView();
            ScrollView.AddToClassList("list-view");
            ScrollView.verticalScroller.value = ScrollView.verticalScroller.highValue > 0 ? ScrollView.verticalScroller.highValue : 0;
            Add(ScrollView);

            DataStoreManager.Messaging.InboxMessageList.DataUpdated += DataUpdatedHandler;
        }

        ~MessageList()
        {
            DataStoreManager.Messaging.InboxMessageList.DataUpdated -= DataUpdatedHandler;
        }

         private void DataUpdatedHandler(GetMessagesBetweenTwoUsersResponse data)
        {
            ScrollView.Clear();
            MessageListEntry lastEntry = null;
            foreach (var message in data.Messages)
            {
                lastEntry = new MessageListEntry(message);
                ScrollView.Add(lastEntry);
            }

            ScrollToLastMessage(lastEntry);
        }

        private void ScrollToLastMessage(VisualElement item)
        {
            if (item == null) return;
            
            var remainingIterations = 4;

            void TryScroll()
            {
                if (item.layout.height > 0 && ScrollView.layout.height > 0)
                {
                    ScrollView.ScrollTo(item);
                    return;
                }

                if (remainingIterations <= 0)
                {
                    Debug.LogWarning("Too many layout iterations");

                    ScrollView.ScrollTo(item);
                    return;
                }

                if (ScrollView.layout.height > 0)
                {
                    item.RegisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged, item);
                }
                else
                {
                    ScrollView.RegisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged, ScrollView);
                }
            }

            void OnGeometryChanged(GeometryChangedEvent evt, VisualElement target)
            {
                target.UnregisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged);

                remainingIterations--;

                TryScroll();
            }

            TryScroll();
        }
    }
}