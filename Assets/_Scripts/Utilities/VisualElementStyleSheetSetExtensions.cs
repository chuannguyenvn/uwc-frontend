using UnityEngine;
using UnityEngine.UIElements;

namespace Utilities
{
    public static class VisualElementStyleSheetSetExtensions
    {
        public static void AddByName(this VisualElementStyleSheetSet visualElementStyleSheet, string stylesheetName)
        {
            var styleSheet = Resources.Load<StyleSheet>($"Stylesheets/{stylesheetName}");
            if (styleSheet == null) Debug.LogWarning($"StyleSheet {stylesheetName} not found");
            else visualElementStyleSheet.Add(styleSheet);
        }
    }
}