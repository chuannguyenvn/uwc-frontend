using System;
using System.Collections.Generic;
using UI.Base;
using UI.Reusables.Control.Sort;

namespace UI.Reusables.Control
{
    public class ListControl : AdaptiveElement
    {
        private SearchBar _searchBar;
        private SortPanel _sortPanel;

        public List<SortType> SortStates => _sortPanel.SortStates;

        public ListControl(Action<string> searched) : base(nameof(ListControl))
        {
            ConfigureUss(nameof(ListControl));

            _searchBar = new SearchBar(searched);
            Add(_searchBar);

            _sortPanel = new SortPanel();
            Add(_sortPanel);
        }

        public void CreateSortButton(string name, Action sortCallback)
        {
            _sortPanel.CreateSortButton(name, sortCallback);
        }
    }
}