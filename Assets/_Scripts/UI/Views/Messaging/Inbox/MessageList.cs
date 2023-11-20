using Commons.Communications.Messages;
using Requests;
using UI.Base;
using UI.Reusables;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class MessageList : AdaptiveElement
    {
        private ScrollViewWithShadow _scrollView;

        public MessageList() : base(nameof(MessageList))
        {
            ConfigureUss(nameof(MessageList));

            CreateScrollView();

            DataStoreManager.Messaging.InboxMessageList.DataUpdated += DataUpdatedHandler;
        }

        ~MessageList()
        {
            DataStoreManager.Messaging.InboxMessageList.DataUpdated -= DataUpdatedHandler;
        }

        private void DataUpdatedHandler(GetMessagesBetweenTwoUsersResponse data)
        {
            _scrollView.Clear();
            MessageListEntry lastEntry = null;
            foreach (var message in data.Messages)
            {
                lastEntry = new MessageListEntry(message);
                _scrollView.AddToScrollView(lastEntry);
            }

            ScrollToLastMessage(lastEntry);
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollViewWithShadow(ShadowType.InnerTop);
            _scrollView.ScrollView.verticalScroller.value =
                _scrollView.ScrollView.verticalScroller.highValue > 0 ? _scrollView.ScrollView.verticalScroller.highValue : 0;
            Add(_scrollView);
        }

        private void ScrollToLastMessage(VisualElement item)
        {
            if (item == null) return;

            var remainingIterations = 4;

            void TryScroll()
            {
                if (item.layout.height > 0 && _scrollView.layout.height > 0)
                {
                    _scrollView.ScrollView.ScrollTo(item);
                    return;
                }

                if (remainingIterations <= 0)
                {
                    Debug.LogWarning("Too many layout iterations");

                    _scrollView.ScrollView.ScrollTo(item);
                    return;
                }

                if (_scrollView.layout.height > 0)
                {
                    item.RegisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged, item);
                }
                else
                {
                    _scrollView.RegisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged, _scrollView);
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