using Commons.Models;
using LocalizationNS;
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
            _modelEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.VehiclesView.MODEL));
            AddContent(_modelEntry);

            _vehicleTypeEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.VehiclesView.VEHICLE_TYPE));
            AddContent(_vehicleTypeEntry);
        }

        public override void SetContent(VehicleData data)
        {
            Title.text = data.LicensePlate;

            _modelEntry.SetValue(data.Model);
            _vehicleTypeEntry.SetValue(Localization.GetVehicleType(data.VehicleType));
        }
    }
}