using Commons.Communications.Vehicles;
using Requests;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Vehicles
{
    public class VehiclesView : View
    {
        private ScrollView _scrollView;
        private VehicleInformationPopup _vehicleInformationPopup;

        public VehiclesView() : base(nameof(VehiclesView))
        {
            ConfigureUss(nameof(VehiclesView));

            AddToClassList("side-view");

            CreateScrollView();
            CreateFullscreenPopup();

            DataStoreManager.Vehicles.AllVehicleList.DataUpdated += DataUpdatedHandler;
        }

        ~VehiclesView()
        {
            DataStoreManager.Vehicles.AllVehicleList.DataUpdated -= DataUpdatedHandler;
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);
        }

        private void CreateFullscreenPopup()
        {
            _vehicleInformationPopup = new VehicleInformationPopup();
            Root.Instance.AddPopup(_vehicleInformationPopup);
        }

        public override void FocusView()
        {
            DataStoreManager.Vehicles.AllVehicleList.Focus();
        }

        public override void UnfocusView()
        {
            DataStoreManager.Vehicles.AllVehicleList.Unfocus();
        }

        private void DataUpdatedHandler(GetAllVehicleResponse response)
        {
            _scrollView.Clear();
            foreach (var vehicleData in response.Vehicles)
            {
                var entry = new VehicleListEntry(vehicleData);
                _scrollView.Add(entry);
                entry.Clicked += () =>
                {
                    _vehicleInformationPopup.SetContent(vehicleData);
                    _vehicleInformationPopup.Show();
                };
            }
        }
    }
}