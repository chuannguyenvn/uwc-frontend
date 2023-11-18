namespace UI.Base
{
    public abstract class DataBasedFullscreenPopup<T> : FullscreenPopup
    {
        public abstract void SetContent(T data);
    }
}