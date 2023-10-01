﻿using UI.Common;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Vehicles
{
    public class VehicleEntry : DataListEntry
    {
        public VehicleEntry()
        {
            name = "VehicleEntry";

            Icon.name = "VehicleIcon";
            PrimaryText.name = "VehicleLicensePlate";
            SecondaryText.name = "VehicleModel";

            SetData("Placeholder Plate", "Placeholder Model");
        }

        public VehicleEntry(string vehicleLicensePlate, string vehicleModel) : this()
        {
            SetData(vehicleLicensePlate, vehicleModel);
        }

        public void SetData(string vehicleLicensePlate, string vehicleModel)
        {
            PrimaryText.text = vehicleLicensePlate;
            SecondaryText.text = vehicleModel;
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<VehicleEntry, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}