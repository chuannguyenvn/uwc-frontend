using System.Collections.Generic;
using Commons.Communications.UserProfiles;
using Requests;
using UI.Base;
using UI.Reusables;
using UI.Reusables.Control;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Workers
{
    public class WorkersView : View
    {
        private ListControl _listControl;
        private ScrollViewWithShadow _scrollView;
        private WorkerInformationPopup _workerInformationPopup;

        private List<WorkerListEntry> _workerListEntries = new();

        public WorkersView() : base(nameof(WorkersView))
        {
            ConfigureUss(nameof(WorkersView));

            AddToClassList("side-view");

            CreateControls();
            CreateScrollView();
            CreateFullscreenPopup();

            DataStoreManager.UserProfile.AllWorkerProfileList.DataUpdated += DataUpdatedHandler;
        }

        ~WorkersView()
        {
            DataStoreManager.UserProfile.AllWorkerProfileList.DataUpdated -= DataUpdatedHandler;
        }

        private void CreateControls()
        {
            _listControl = new ListControl(SearchHandler);
            Add(_listControl);
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollViewWithShadow(ShadowType.InnerTop) { name = "ScrollView" };
            Add(_scrollView);
        }

        private void CreateFullscreenPopup()
        {
            _workerInformationPopup = new WorkerInformationPopup();
            Root.Instance.AddPopup(_workerInformationPopup);
        }

        public override void FocusView()
        {
            DataStoreManager.UserProfile.AllWorkerProfileList.Focus();
        }

        public override void UnfocusView()
        {
            DataStoreManager.UserProfile.AllWorkerProfileList.Unfocus();
        }

        private void DataUpdatedHandler(GetAllWorkerProfilesResponse response)
        {
            _scrollView.Clear();
            _workerListEntries.Clear();
            foreach (var userProfile in response.WorkerProfiles)
            {
                var entry = new WorkerListEntry(userProfile);
                _scrollView.AddToScrollView(entry);
                _workerListEntries.Add(entry);

                entry.Clicked += () =>
                {
                    _workerInformationPopup.SetContent(userProfile);
                    _workerInformationPopup.Show();
                };
            }
        }

        private void SearchHandler(string text)
        {
            text = Utility.CreateSearchString(text);
            foreach (var entry in _workerListEntries)
            {
                if (Utility.CreateSearchString(entry.Profile.FirstName, entry.Profile.LastName, entry.Profile.Address,
                        entry.Profile.Gender.ToString(), entry.Profile.UserRole.ToString(), entry.Profile.DateOfBirth.ToString()).Contains(text) ||
                    text == "")
                {
                    entry.style.display = DisplayStyle.Flex;
                }
                else
                {
                    entry.style.display = DisplayStyle.None;
                }
            }
        }
    }
}