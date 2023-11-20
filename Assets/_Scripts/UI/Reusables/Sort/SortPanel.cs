using System;
using System.Collections.Generic;
using UI.Base;

namespace UI.Reusables.Sort
{
    public class SortPanel : AdaptiveElement
    {
        public List<SortType> SortStates { get; } = new List<SortType>();
        private List<SortButton> _sortButtons = new();

        public SortPanel() : base(nameof(SortPanel))
        {
            ConfigureUss(nameof(SortPanel));
        }

        public void CreateSortButton(string name, Action sortCallback)
        {
            var sortButton = new SortButton(this, SortStates.Count, name, sortCallback);
            _sortButtons.Add(sortButton);
            SortStates.Add(SortType.None);
            Add(sortButton);
        }
    }
}