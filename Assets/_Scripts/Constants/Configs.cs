using System.Collections.Generic;

namespace Constants
{
    public static class Configs
    {
#if UNITY_2023
        public const bool IS_DESKTOP = true;
#elif UNITY_WEBGL || UNITY_STANDALONE
        public const bool IS_DESKTOP = true;
#elif UNITY_ANDROID
        public const bool IS_DESKTOP = false;
#endif

        public static readonly List<ViewType> DesktopViewTypes = new()
        {
            ViewType.Map,
            ViewType.Workers,
            ViewType.Mcps,
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