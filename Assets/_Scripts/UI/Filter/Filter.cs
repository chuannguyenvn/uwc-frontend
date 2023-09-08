using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Filter
{
    public class Filter : VisualElement
    {
        private readonly Image _filterIcon;
        private readonly TextElement _text;

        public Filter()
        {
            AddToClassList("filter");

            _filterIcon = new Image { name = "FilterIcon" };
            _filterIcon.AddToClassList("icon");
            _filterIcon.AddToClassList("dark-icon");
            Add(_filterIcon);

            _text = new TextElement { name = "Text", text = "Filter" };
            Add(_text);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<Filter, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}