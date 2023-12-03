using System;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.Control.Sort
{
    public class SortButton : AnimatedButton
    {
        private readonly SortPanel _sortPanel;
        private readonly int _index;
        private SortType _sortType = SortType.None;

        private TextElement _sortNameText;
        private VisualElement _sortIcon;

        public SortButton(SortPanel sortPanel, int index, string name, Action sortCallback)
        {
            _sortPanel = sortPanel;
            _index = index;

            ConfigureUss(nameof(SortButton));

            AddToClassList("iconless-button");
            AddToClassList("rounded-button-16px");
            AddToClassList("white-button");

            CreateSortName(name);
            CreateSortIcon();

            RegisterClickedEvent(sortCallback);
        }

        private void CreateSortName(string name)
        {
            _sortNameText = new TextElement { name = "SortNameText" };
            _sortNameText.AddToClassList("sub-text");
            _sortNameText.AddToClassList("grey-text");
            _sortNameText.text = name;
            Add(_sortNameText);
        }

        private void CreateSortIcon()
        {
            _sortIcon = new VisualElement { name = "SortIcon" };
            _sortIcon.style.display = DisplayStyle.None;
            Add(_sortIcon);
        }

        private void RegisterClickedEvent(Action sortCallback)
        {
            Clicked += () =>
            {
                switch (_sortType)
                {
                    case SortType.None:
                        _sortType = SortType.Descending;

                        AddToClassList("descending");
                        RemoveFromClassList("ascending");
                        RemoveFromClassList("none");

                        _sortIcon.style.display = DisplayStyle.Flex;

                        _sortNameText.AddToClassList("colored-text");
                        _sortNameText.RemoveFromClassList("grey-text");
                        break;
                    case SortType.Descending:
                        _sortType = SortType.Ascending;

                        AddToClassList("ascending");
                        RemoveFromClassList("descending");
                        RemoveFromClassList("none");

                        _sortIcon.style.display = DisplayStyle.Flex;

                        _sortNameText.AddToClassList("colored-text");
                        _sortNameText.RemoveFromClassList("grey-text");
                        break;
                    case SortType.Ascending:
                        _sortType = SortType.None;

                        AddToClassList("none");
                        RemoveFromClassList("ascending");
                        RemoveFromClassList("descending");

                        _sortIcon.style.display = DisplayStyle.None;

                        _sortNameText.RemoveFromClassList("colored-text");
                        _sortNameText.AddToClassList("grey-text");
                        break;
                }

                _sortPanel.SortStates[_index] = _sortType;
                sortCallback?.Invoke();
            };
        }
    }

    public enum SortType
    {
        None,
        Ascending,
        Descending
    }
}