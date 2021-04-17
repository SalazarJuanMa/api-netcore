namespace APP.Constants
{
    public static class StartupConstants
    {
        public const string ACCEPT = "Accept";
        public const string ALLOWED_ORIGIN = "ALLOWED_ORIGIN";
        public const string ALLOWED_ORIGINAL_POLICY = "_allowedOriginPolicy";
        public const string API = "api";
        public const string API_BASE_URL = "API_BASE_URL";
        public const string APP = "offersetupapp";
        public const string CONTENT_TYPE = "Content-Type";
        public const string CONTENT_TYPE_JSON = "application/json";
        public const string CONTROLLER_ROUTE = "{controller}/{action=Index}/{id?}";
        public const string CORS = "Cors";
        public const string DEFAULT = "helios";
        public const string HEALTH_CHECK = "/health";


        /// <summary>
        /// Class Swagger.
        /// </summary>
        public static class Swagger
        {
            public const string CONTACT_URL = "https://ecstatic-allen-72d7e6.netlify.app/";
            public const string DESCRIPTION = "Master Model Operations API";
            public const string LICENSE_URL = "https://ecstatic-allen-72d7e6.netlify.app/";
            public const string NAME = "Master Model Operations API V1";
            public const string POLICY = "Privacy Policy";
            public const string SUPPORT = "Support";
            public const string TERM_SERVICE_URL = "https://ecstatic-allen-72d7e6.netlify.app//";
            public const string TITLE = "Master Model Apps";
            public const string URL_JSON = "/swagger/v1/swagger.json";
            public const string VERSION = "v1";
            public const string EMAIL = "licjuanma@hotmail.com";
            public const string OFFER_OPERATIONS = "api";

            public static class ResponseHeader
            {
                public const string LOCATION_NAME = "Location";
                public const string LOCATION_DESCRIPTION = "Location of the newly created resource";
                public const string DATE_NAME = "Date";
                public const string DATE_DESCRIPTION = "The UTC date/time at which the current rate limit window resets.";
                public const string CONTENT_TYPE = "Content-Type";
                public const string CONTENT_DESCRIPTION = "application/json";
                public const string STRING_TYPE = "string";

            }
        }
    }
}
