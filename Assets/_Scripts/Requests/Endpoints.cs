namespace Requests
{
    public static class Endpoints
    {
        // Cloud: urban-waste-collection.azurewebsites.net
        // Local: localhost:44394
        public const string DOMAIN = "urban-waste-collection.azurewebsites.net";
        
        public static class Authentication
        {
            private const string AUTHENTICATION_ENDPOINT = DOMAIN + "/authentication";

            public const string LOGIN = AUTHENTICATION_ENDPOINT + "/login";
            public const string REGISTER = AUTHENTICATION_ENDPOINT + "/register";
        }

        public static class McpData
        {
            private const string MCP_DATA_ENDPOINT = DOMAIN + "/mcpdata";

            public const string GET = MCP_DATA_ENDPOINT + "/get";
        }
    }
}