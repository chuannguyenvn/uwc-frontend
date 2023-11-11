using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Reusables
{
    public class SearchBar : AdaptiveElement
    {
        private TextField _searchField;
        private Button _searchButton;
        private VisualElement _searchIcon;
        
        public SearchBar() : base(nameof(SearchBar))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Reusables/SearchBar"));
            AddToClassList("search-bar");
            
            _searchField = new TextField { name = "SearchField" };
            _searchField.AddToClassList("normal-text");
            _searchField.AddToClassList("black-text");
            _searchField.textEdition.placeholder = "Search...";
            Add(_searchField);
            
            _searchButton = new Button { name = "SearchButton" };
            _searchField.Add(_searchButton);
            
            _searchIcon = new VisualElement { name = "SearchIcon" };
            _searchButton.Add(_searchIcon);
        }
        
        public new class UxmlFactory : UxmlFactory<SearchBar, UxmlTraits>
        {
        }
    }
}