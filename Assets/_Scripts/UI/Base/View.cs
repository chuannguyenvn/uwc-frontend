namespace UI.Base
{
    public abstract class View : AdaptiveElement
    {
        protected View(string name) : base(name)
        {
            ConfigureUss(nameof(View));
        }

        public virtual void FocusView()
        {
        }

        public virtual void UnfocusView()
        {
        }
    }
}