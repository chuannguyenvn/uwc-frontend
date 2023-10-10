namespace Requests
{
    public static class Endpoints
    {
        private const string DOMAIN = "urban-waste-collection.azurewebsites.net";

        public static class McpData
        {
            private const string MCP_DATA_ENDPOINT = DOMAIN + "/mcpdata";

            public const string GET = MCP_DATA_ENDPOINT + "/get";
        }
    }
}