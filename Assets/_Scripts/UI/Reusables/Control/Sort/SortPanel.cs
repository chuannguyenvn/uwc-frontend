﻿using System;
using System.Collections.Generic;
using LocalizationNS;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.Control.Sort
{
    public class SortPanel : AdaptiveElement
    {
        public List<SortType> SortStates { get; } = new List<SortType>();

        private TextElement _titleText;
        private List<SortButton> _sortButtons = new();

        public SortPanel() : base(nameof(SortPanel))
        {
            ConfigureUss(nameof(SortPanel));

            CreateTitleText();
        }

        private void CreateTitleText()
        {
            _titleText = new TextElement { name = "TitleText" };
            _titleText.AddToClassList("sub-text");
            _titleText.AddToClassList("grey-text");
            _titleText.text = Localization.GetSentence(Sentence.ListControl.SORT_BY) + ":";
            _titleText.style.display = DisplayStyle.None;
            Add(_titleText);
        }

        public void CreateSortButton(string name, Action sortCallback)
        {
            var sortButton = new SortButton(this, SortStates.Count, name, sortCallback);
            _sortButtons.Add(sortButton);
            SortStates.Add(SortType.None);
            Add(sortButton);
            
            _titleText.style.display = DisplayStyle.Flex;
        }
    }
}