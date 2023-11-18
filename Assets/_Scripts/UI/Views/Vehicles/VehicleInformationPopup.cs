using Commons.Models;
using UI.Base;

namespace UI.Views.Vehicles
{
    public class VehicleInformationPopup : DataBasedFullscreenPopup<VehicleData>
    {
        private PopupInformationEntry _modelEntry;
        private PopupInformationEntry _vehicleTypeEntry;

        public VehicleInformationPopup()
        {
            ConfigureUss(nameof(VehicleInformationPopup));

            CreateDetails();
        }

        private void CreateDetails()
        {
            _modelEntry = new PopupInformationEntry("Model");
            AddContent(_modelEntry);

            _vehicleTypeEntry = new PopupInformationEntry("Vehicle type");
            AddContent(_vehicleTypeEntry);
        }

        public override void SetContent(VehicleData data)
        {
            Title.text = data.LicensePlate;

            _modelEntry.SetValue(data.Model);
            _vehicleTypeEntry.SetValue(data.VehicleType.ToString());
        }
    }
}