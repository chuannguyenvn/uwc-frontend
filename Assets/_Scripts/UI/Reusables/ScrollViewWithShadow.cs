using System;
using Requests;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Reusables
{
    public class ScrollViewWithShadow : AdaptiveElement
    {
        private readonly Action<ScrollView> _scrolled;
        private VisualElement _shadow;
        private ScrollView _scrollView;
        private float _oldVerticalScrollerValue;

        public ScrollViewWithShadow(ShadowType shadowType, Action<ScrollView> scrolled = null) : base(nameof(ScrollViewWithShadow))
        {
            _scrolled = scrolled;

            ConfigureUss(nameof(ScrollViewWithShadow));

            CreateShadow(shadowType);
            CreateScrollView();

            _scrollView.verticalScroller.valueChanged += (_) => _scrolled?.Invoke(_scrollView);
        }

        private void CreateShadow(ShadowType shadowType)
        {
            _shadow = new VisualElement { name = "Shadow" };
            _shadow.AddToClassList("shadow");
            Add(_shadow);

            _shadow.pickingMode = PickingMode.Ignore;

            switch (shadowType)
            {
                case ShadowType.InnerTop:
                    _shadow.AddToClassList("inner-top");
                    break;
                case ShadowType.InnerBottom:
                    _shadow.AddToClassList("inner-bottom");
                    break;
                case ShadowType.InnerLeft:
                    _shadow.AddToClassList("inner-left");
                    break;
                case ShadowType.InnerRight:
                    _shadow.AddToClassList("inner-right");
                    break;
            }
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollView { name = "ScrollView" };
            Add(_scrollView);
        }

        public void AddToScrollView(VisualElement element)
        {
            _scrollView.Add(element);
            _shadow.BringToFront();
        }

        public new void Clear()
        {
            _scrollView.Clear();
        }

        public void ScrollToLast()
        {
            _scrollView.contentContainer.RegisterCallback<GeometryChangedEvent>(ScrollToLastHandler);
        }

        private void ScrollToLastHandler(GeometryChangedEvent evt)
        {
            ((VisualElement)evt.target).UnregisterCallback<GeometryChangedEvent>(ScrollToLastHandler);
            _scrollView.verticalScroller.value = _scrollView.verticalScroller.highValue;
        }

        public void MarkOldVerticalScrollerValue()
        {
            _oldVerticalScrollerValue = _scrollView.verticalScroller.highValue;
        }

        public void ScrollToOldVerticalScrollerValue()
        {
            _scrollView.contentContainer.RegisterCallback<GeometryChangedEvent>(ScrollToOldVerticalScrollerValueHandler);
        }

        private void ScrollToOldVerticalScrollerValueHandler(GeometryChangedEvent evt)
        {
            ((VisualElement)evt.target).UnregisterCallback<GeometryChangedEvent>(ScrollToOldVerticalScrollerValueHandler);
            _scrollView.verticalScroller.value = _scrollView.verticalScroller.highValue - _oldVerticalScrollerValue;
        }


        private void ScrollToImmediate(VisualElement item)
        {
            int remainingIterations = 4;

            void TryScroll()
            {
                //if both layout and item have a size, then we can scroll immediate
                //otherwise, we need to listen to layout changes then scrollTo

                if (item.layout.width > 0 && _scrollView.layout.width > 0)
                {
                    _scrollView.ScrollTo(item);
                    return;
                }
                else if (remainingIterations <= 0)
                {
                    Debug.LogWarning("Too many layout iterations");

                    _scrollView.ScrollTo(item);
                    return;
                }

                if (_scrollView.layout.width > 0)
                {
                    item.RegisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged, item);
                }
                else
                {
                    _scrollView.RegisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged, _scrollView);
                }
            }

            void OnGeometryChanged(GeometryChangedEvent evt, VisualElement target)
            {
                target.UnregisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged);

                //try scrolling after we received a geometry changed event from either the item or scrollView
                //the geometry still might be 0, so keep trying if so

                remainingIterations--;

                TryScroll();
            }

            TryScroll();
        }
    }

    public enum ShadowType
    {
        None,
        InnerTop,
        InnerBottom,
        InnerLeft,
        InnerRight,
    }
}