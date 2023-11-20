using System.Collections.Generic;
using Commons.Communications.Vehicles;
using Requests;
using UI.Base;
using UI.Reusables;
using UI.Reusables.Control;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Vehicles
{
    public class VehiclesView : View
    {
        private ListControl _listControl;
        private ScrollViewWithShadow _scrollView;
        private VehicleInformationPopup _vehicleInformationPopup;

        private List<VehicleListEntry> _vehicleListEntries = new();

        public VehiclesView() : base(nameof(VehiclesView))
        {
            ConfigureUss(nameof(VehiclesView));

            AddToClassList("side-view");

            CreateControls();
            CreateScrollView();
            CreateFullscreenPopup();

            DataStoreManager.Vehicles.AllVehicleList.DataUpdated += DataUpdatedHandler;
        }

        ~VehiclesView()
        {
            DataStoreManager.Vehicles.AllVehicleList.DataUpdated -= DataUpdatedHandler;
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
            _vehicleListEntries.Clear();
            foreach (var vehicleData in response.Vehicles)
            {
                var entry = new VehicleListEntry(vehicleData);

                _scrollView.AddToScrollView(entry);
                _vehicleListEntries.Add(entry);

                entry.Clicked += () =>
                {
                    _vehicleInformationPopup.SetContent(vehicleData);
                    _vehicleInformationPopup.Show();
                };
            }
        }

        private void SearchHandler(string text)
        {
            text = Utility.CreateSearchString(text);
            foreach (var entry in _vehicleListEntries)
            {
                if (Utility.CreateSearchString(entry.VehicleData.LicensePlate, entry.VehicleData.VehicleType.ToString(), entry.VehicleData.Model)
                        .Contains(text) || text == "")
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