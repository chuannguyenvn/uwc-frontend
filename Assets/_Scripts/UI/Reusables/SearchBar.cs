using System;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Reusables
{
    public class SearchBar : AdaptiveElement
    {
        private readonly Action<string> _searched;
        
        private TextField _searchField;
        private Button _searchButton;
        private VisualElement _searchIcon;

        public SearchBar(Action<string> searched) : base(nameof(SearchBar))
        {
            _searched = searched;

            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Reusables/SearchBar"));
            AddToClassList("search-bar");

            CreateSearchField();
            CreateSearchIcon();
        }

        private void CreateSearchField()
        {
            _searchField = new TextField { name = "SearchField" };
            _searchField.AddToClassList("normal-text");
            _searchField.AddToClassList("black-text");
            _searchField.textEdition.placeholder = "Search...";
            Add(_searchField);

            _searchField.RegisterCallback<ChangeEvent<string>>(e => Search());
        }

        private void CreateSearchIcon()
        {
            _searchButton = new Button { name = "SearchButton" };
            _searchField.Add(_searchButton);

            _searchIcon = new VisualElement { name = "SearchIcon" };
            _searchButton.Add(_searchIcon);
        }

        private void Search()
        {
            _searched?.Invoke(_searchField.text);
        }
    }
}