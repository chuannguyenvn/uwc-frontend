using UnityEngine.UIElements;

namespace UI.Base
{
    public abstract class DataBasedFullscreenPopup<T> : FullscreenPopup
    {
        protected readonly TextElement Title;
        protected readonly VisualElement DetailContainer;

        protected DataBasedFullscreenPopup()
        {
            Title = new TextElement { name = "Title" };
            Title.AddToClassList("super-title-text");
            Title.AddToClassList("black-text");
            AddContent(Title);

            DetailContainer = new VisualElement { name = "DetailContainer" };
            AddContent(DetailContainer);
        }

        public abstract void SetContent(T data);
    }
}