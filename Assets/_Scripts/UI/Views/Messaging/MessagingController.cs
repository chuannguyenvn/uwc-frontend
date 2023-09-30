using Constants;
using UI.Main;
using UI.Views.Messaging.Contacts;
using UI.Views.Messaging.Inbox;
using UnityEngine.UIElements;

namespace UI.Views.Messaging
{
    public class MessagingController : Singleton<MessagingController>
    {
        private ContactsSubview _contactsSubview;
        private InboxSubview _inboxSubview;

        protected override void Awake()
        {
            base.Awake();
            QueryElements();

            if (Configs.IS_DESKTOP)
            {
            }
            else
            {
                HideInbox();
            }
        }

        private void QueryElements()
        {
            var messagingView = MainController.Instance.ViewsByViewType[ViewType.Messaging];

            _contactsSubview = messagingView.Q<ContactsSubview>();
            _inboxSubview = messagingView.Q<InboxSubview>();

            if (Configs.IS_DESKTOP)
            {
            }
            else
            {
                _inboxSubview.Q("BackButton").RegisterCallback<MouseUpEvent>(_ => HideInbox());
            }
        }

        public void ShowInbox()
        {
            _contactsSubview.style.display = DisplayStyle.None;
            _inboxSubview.style.display = DisplayStyle.Flex;
        }

        public void HideInbox()
        {
            _contactsSubview.style.display = DisplayStyle.Flex;
            _inboxSubview.style.display = DisplayStyle.None;
        }
    }
}