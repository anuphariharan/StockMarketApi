using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockMarket.InfoProvider.Robinhood.Contracts;
using StockMarket.InfoProvider.Robinhood.Contracts.Requests;


namespace Robinhood.Client
{
    public static partial class RobinhoodClient
    {
        public static async Task<Quote> GetQuote(string symbol)
        {
            var address = new UriBuilder
            {
                Scheme = "https",
                Host = ApiAddress.RootUri,
                Path = $"{ApiAddress.Quotes}/{symbol.ToUpperInvariant()}/"
            }.Uri;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = address
            };
            var response = await MakeRequest(request);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Quote>(responseBody, JsonSettings);
        }

        public static async Task<IEnumerable<Quote>> GetQuotes(IEnumerable<string> symbols)
        {
            var sb = new StringBuilder();
            sb.Append("?symbols=");
            foreach (var symbol in symbols)
            {
                sb.Append(symbol.ToUpperInvariant());
                sb.Append(",");
            }

            sb.Remove(sb.Length - 1, 1);
            var address = new UriBuilder
            {
                Scheme = "https",
                Host = ApiAddress.RootUri,
                Path = $"{ApiAddress.Quotes}/",
                Query = sb.ToString()
            }.Uri;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = address
            };
            var response = await MakeRequest(request);
            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseBody);
            if (json["results"] is JArray resultArray)
                return resultArray.ToObject<List<Quote>>(JsonSerializer.Create(JsonSettings));
            throw new HttpRequestException("An error occured while sending the request");
        }
    }
}