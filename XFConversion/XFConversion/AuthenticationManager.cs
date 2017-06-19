using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

namespace XFConversion
{
    public class AuthenticationManager
    {
        private static IPlatformParameters _parameters;
        private AuthenticationResult _authenticationResult;

        public UserInfo UserInfo { get { return _authenticationResult?.UserInfo; } }

        public static void SetConfiguration()
        {
            var authContext = new AuthenticationContext(Configuration.Authority);
            authContext.TokenCache.Clear();
        }

        public static void SetParameters(IPlatformParameters parameters)
        {
            _parameters = parameters;
        }

        public async Task LoginAsync()
        {
            var auth = DependencyService.Get<IAuthenticator>();
            _authenticationResult = await auth.Authenticate(Configuration.Authority, Configuration.Resource,
                Configuration.ClientId, Configuration.RedirectUri);

        }

        public void Logout()
        {
            var auth = DependencyService.Get<IAuthenticator>();
            auth.Logout(Configuration.Authority, Configuration.Resource,Configuration.ClientId);

            _authenticationResult = null;
        }

        public HttpClient CreateHttpClient()
        {
            #region for proxy to see traffic in fiddler 

            //var uri = new Uri(Configuration.ProxyForFiddler);
            //var handler = new HttpClientHandler
            //{
            //    Proxy = new  //Proxy(uri),
            //    UseProxy = true
            //};

            // var client = new HttpClient(handler);

            #endregion


            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(_authenticationResult?.AccessTokenType, _authenticationResult?.AccessToken);

            return client;
        }
        
    }
}
