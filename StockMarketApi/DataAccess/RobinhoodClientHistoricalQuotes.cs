using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StockMarket.InfoProvider.Robinhood.Contracts;
using StockMarket.InfoProvider.Robinhood.Contracts.Enum;
using StockMarket.InfoProvider.Robinhood.Contracts.Internal;
using StockMarket.InfoProvider.Robinhood.Contracts.Requests;

namespace Robinhood.Client
{
    public static partial class RobinhoodClient
    {
        public static async Task<HistoricalQuote[]> GetHistoricalQuote(HistoricalQuoteRequest request)
        {
            var address = new UriBuilder
            {
                Scheme = "https",
                Host = ApiAddress.RootUri,
                Path = $"{ApiAddress.HistoricalQuotes}/{request.Symbol.ToUpperInvariant()}/",
                Query = GetQuery(request)
            }.Uri;
            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = address
            };
            var response = await MakeRequest(httpRequest);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HistoricalQuoteResponse>(responseBody, JsonSettings).Historicals;
        }

        private static string GetQuery(HistoricalQuoteRequest request)
        {
            var span = GetSpan(request.Span);
            var interval = GetInterval(request.Interval);
            var bounds = GetBounds(request.Bounds);
            var query = "?interval=" + interval;
            query += "&span=" + span;
            query += "&bounds=" + bounds;
            return query;
        }

        private static string GetSpan(HistoricalSpan requestSpan)
        {
            switch (requestSpan)
            {
                case HistoricalSpan.Day:
                    return "day";
                case HistoricalSpan.Week:
                    return "week";
                case HistoricalSpan.Year:
                    return "year";
                case HistoricalSpan.FiveYear:
                    return "5year";
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestSpan), requestSpan, null);
            }
        }

        private static string GetInterval(HistoricalInterval requestInterval)
        {
            switch (requestInterval)
            {
                case HistoricalInterval.Day:
                    return "day";
                case HistoricalInterval.Week:
                    return "week";
                case HistoricalInterval.TenMinute:
                    return "10minute";
                case HistoricalInterval.FiveMinute:
                    return "5minute";
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestInterval), requestInterval, null);
            }
        }

        private static string GetBounds(HistoricalBounds requestBounds)
        {
            switch (requestBounds)
            {
                case HistoricalBounds.Extended:
                    return "extended";
                case HistoricalBounds.Regular:
                    return "regular";
                case HistoricalBounds.Trading:
                    return "trading";
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestBounds), requestBounds, null);
            }
        }
    }
}