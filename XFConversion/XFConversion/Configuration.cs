namespace XFConversion
{
    public static class Configuration
    {
        public const string ClientId = "00000000-0000-0000-0000-000000000000";  // Put your mobile app ClientId
        public const string Authority = "https://login.windows.net/bp.com/";    // Default authority for Azure AD
        public const string Resource = "00000000-0000-0000-0000-000000000000";  // Put your API ID URI 
        public const string RedirectUri = "https://tennent/mobile-app-name";    // Put your mobile app Redirect Uri (declared in Azure AD Apps)

        public const string ApiUri = "https://webapi.endpoint.com/api/blah";    // Put you API actual URL
        //public const string ProxyForFiddler = "10.199.4.1:8888"; // Put you API actual URL
    }

    



}
