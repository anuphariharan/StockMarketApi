using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StockMarket.InfoProvider.Robinhood.Contracts.Internal;
using StockMarket.InfoProvider.Robinhood.Contracts.Requests;


namespace Robinhood.Client
{
    public static partial class RobinhoodClient
    {
        private static readonly HttpClient Http = new HttpClient(new HttpClientHandler
        {
            AllowAutoRedirect = false
        });

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };




        public async static Task<bool> Init(IConfiguration configuration)
        {
            try
            {
                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Http.DefaultRequestHeaders.Add("User-Agent", configuration.GetSection("StockMarketInfoProvider:Robinhood:UserAgent").Value);



                ApiAddress.RootUri = configuration.GetSection("StockMarketInfoProvider:Robinhood:RootUri").Value;
                ApiAddress.Login = configuration.GetSection("StockMarketInfoProvider:Robinhood:Login").Value;
                ApiAddress.Quotes = configuration.GetSection("StockMarketInfoProvider:Robinhood:Quotes").Value;
                ApiAddress.HistoricalQuotes = configuration.GetSection("StockMarketInfoProvider:Robinhood:HistoricalQuotes").Value;

                string username = configuration.GetSection("StockMarketInfoProvider:Robinhood:Username").Value;
                string password = configuration.GetSection("StockMarketInfoProvider:Robinhood:Password").Value;

                await Login(username, password);
            }
            catch
            {

                throw;
            }
            return true;
        }

        private static async Task Login(string username, string password)
        {



            
            var uri = new UriBuilder
            {
                Scheme = "https",
                Host = ApiAddress.RootUri,
                Path = ApiAddress.Login
            }.Uri;
            var postBody = new
            {
                username,
                password
            };
            var request = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postBody), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post,
                RequestUri = uri
            };
            var response = await MakeRequest(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(body);
            if (tokenResponse.MfaRequired)
            {
                throw new NotSupportedException("MFA login is not supported at this time.");
            }

            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", tokenResponse.Token);
        }

        private static async Task<HttpResponseMessage> MakeRequest(HttpRequestMessage request)
        {
            while (true)
            {
                var response = await Http.SendAsync(request);
                var statusCode = (int)response.StatusCode;

                // We want to handle redirects ourselves so that we can determine the final redirect Location (via header)
                if (statusCode < 300 || statusCode > 399)
                {
                    return response.IsSuccessStatusCode
                        ? response
                        : throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }

                var redirectUri = response.Headers.Location;
                if (!redirectUri.IsAbsoluteUri)
                {
                    redirectUri = new System.Uri(request.RequestUri.GetLeftPart(UriPartial.Authority) + redirectUri);
                }

                var newRequest = new HttpRequestMessage
                {
                    Content = request.Content,
                    RequestUri = redirectUri,
                    Method = request.Method,
                    Version = request.Version
                };
                request = newRequest;
            }
        }
    }
}