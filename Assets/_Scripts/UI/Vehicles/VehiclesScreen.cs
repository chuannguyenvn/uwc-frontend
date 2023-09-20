using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Vehicles
{
    public class VehiclesScreen : VisualElement
    {
        private VehiclesDataList _vehiclesDataList;

        public VehiclesScreen()
        {
            name = "VehiclesScreen";

            _vehiclesDataList = new VehiclesDataList();
            Add(_vehiclesDataList);
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