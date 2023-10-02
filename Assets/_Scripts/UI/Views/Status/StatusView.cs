using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Status
{
    public class StatusView : View
    {
        public PanelList PanelList;

        public StatusView() : base(nameof(StatusView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Status/StatusView"));
            AddToClassList("full-view");
            
                        
            PanelList = new PanelList();
            Add(PanelList);
            
            PanelList.Add(new PersonalInformationPanel());
            PanelList.Add(new VehicleInformationPanel());
        }
    }
}