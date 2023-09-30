﻿using Constants;
using UI.Common;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Vehicles
{
    public class VehiclesDataList : DataList
    {
        public VehiclesDataList()
        {
            name = "VehiclesList";
            
            if (Configs.IS_DEBUGGING)
                for (var i = 0; i < 20; i++)
                    Add(new VehicleEntry());
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<VehiclesDataList, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : DataList.UxmlTraits
        {
        }

        #endregion
    }
}