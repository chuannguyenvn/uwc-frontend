namespace Requests
{
    public static class Endpoints
    {
        public const string DOMAIN = "localhost:44394";

        public static class McpData
        {
            private const string MCP_DATA_ENDPOINT = DOMAIN + "/mcpdata";

            public const string GET = MCP_DATA_ENDPOINT + "/get";
        }
    }
}