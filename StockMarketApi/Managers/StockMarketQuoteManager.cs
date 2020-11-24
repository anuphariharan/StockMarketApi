using Robinhood.Client;
using System;
using System.Collections.Generic;
using System.Text;
using StockMarket.InfoProvider.Robinhood.Contracts.Requests;
using StockMarket.InfoProvider.Robinhood.Contracts;
using System.Threading.Tasks;

namespace MA.Content.Services.StockMarketApi.Managers
{
    public class StockMarketQuoteManager : IStockMarketQuoteManager
    {
        public async Task<HistoricalQuote[]> GetHistoricalQuote(HistoricalQuoteRequest request) => await RobinhoodClient.GetHistoricalQuote(request);
        public async Task<Quote> GetQuote(string symbol) => await RobinhoodClient.GetQuote(symbol);
        public async Task<IEnumerable<Quote>> GetQuotes(List<string> request) => await RobinhoodClient.GetQuotes(request);
    }
}
