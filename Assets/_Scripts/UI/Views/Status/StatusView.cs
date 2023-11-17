using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Status
{
    public class StatusView : View
    {
        // Panels
        private PanelList _panelList;
        private PersonalInformationPanel _personalInformationPanel;
        private VehicleInformationPanel _vehicleInformationPanel;

        public StatusView() : base(nameof(StatusView))
        {
            ConfigureUss(nameof(StatusView));

            AddToClassList("full-view");

            CreatePanels();
        }

        private void CreatePanels()
        {
            _panelList = new PanelList();
            Add(_panelList);

            _personalInformationPanel = new PersonalInformationPanel();
            _panelList.AddPanel(_personalInformationPanel);

            _vehicleInformationPanel = new VehicleInformationPanel();
            _panelList.AddPanel(_vehicleInformationPanel);
        }
    }
}