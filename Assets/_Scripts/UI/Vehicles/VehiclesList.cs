using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Vehicles
{
    public class VehiclesList : ScrollView
    {
        public VehiclesList()
        {
            name = "VehiclesList";
            AddToClassList("list");
            for (var i = 0; i < 20; i++) Add(new VehicleEntry());
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<VehiclesList, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : ScrollView.UxmlTraits
        {
        }

        #endregion
    }
}