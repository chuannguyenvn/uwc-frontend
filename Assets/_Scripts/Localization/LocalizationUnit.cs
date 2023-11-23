namespace Localization
{
    public class LocalizationUnit
    {
        public string English { get; }
        public string Vietnamese { get; }

        public LocalizationUnit(string english, string vietnamese)
        {
            English = english;
            Vietnamese = vietnamese;
        }
    }
}