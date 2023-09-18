using Commons.Categories;

namespace Constants
{
    public static class Endpoints
    {
        public const string DOMAIN = "localhost:44394/";

        public static class Account
        {
            private const string AUTHENTICATION = "authentication/";

            public const string LOGIN = DOMAIN + AUTHENTICATION + "login/";
            public const string REGISTER = DOMAIN + AUTHENTICATION + "register/";
        }
    }
}