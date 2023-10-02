using System.Collections.Generic;

namespace Constants
{
    public static class Configs
    {
        public const bool IS_DESKTOP = true;
        
        public static readonly List<ViewType> DesktopViewTypes = new()
        {
            ViewType.Map,
            ViewType.Workers,
            ViewType.MCPs,
            ViewType.Vehicles,
            ViewType.Reporting,
            ViewType.Messaging,
            ViewType.Settings,
        }; 
        
        public static readonly List<ViewType> MobileViewTypes = new()
        {
            ViewType.Map,
            ViewType.Tasks,
            ViewType.Status,
            ViewType.Messaging,
            ViewType.Settings,
        };
    }
}