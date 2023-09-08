using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Vehicles
{
    public class VehiclesScreen : VisualElement
    {
        private VehiclesList _vehiclesList;

        public VehiclesScreen()
        {
            name = "VehiclesScreen";

            _vehiclesList = new VehiclesList();
            Add(_vehiclesList);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<VehiclesScreen, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}