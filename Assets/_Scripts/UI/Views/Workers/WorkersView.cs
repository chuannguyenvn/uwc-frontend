using System.Collections.Generic;
using Commons.Communications.UserProfiles;
using Commons.Models;
using Requests;
using UI.Base;
using UI.Views.Mcps;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkersView : View
    {
        private ScrollView _scrollView;
        private WorkerInformationPopup _workerInformationPopup;

        public WorkersView() : base(nameof(WorkersView))
        {
            ConfigureUss(nameof(WorkersView));

            AddToClassList("side-view");

            CreateScrollView();
            CreateFullscreenPopup();

            DataStoreManager.UserProfile.AllWorkerProfileList.DataUpdated += DataUpdatedHandler;
        }

        ~WorkersView()
        {
            DataStoreManager.UserProfile.AllWorkerProfileList.DataUpdated -= DataUpdatedHandler;
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
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
            foreach (var userProfile in response.WorkerProfiles)
            {
                var entry = new WorkerListEntry(userProfile);
                _scrollView.Add(entry);
                entry.Clicked += () =>
                {
                    _workerInformationPopup.SetContent(userProfile);
                    _workerInformationPopup.Show();
                };
            }
        }
    }
}