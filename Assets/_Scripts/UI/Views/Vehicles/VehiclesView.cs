﻿using UI.Common;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Vehicles
{
    public class VehiclesView : View
    {
        private VehiclesDataList _vehiclesDataList;

        public VehiclesView() : base("Vehicles")
        {
            _vehiclesDataList = new VehiclesDataList();
            Add(_vehiclesDataList);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<VehiclesView, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}