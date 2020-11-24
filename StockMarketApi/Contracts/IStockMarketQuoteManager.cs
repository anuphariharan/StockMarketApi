using StockMarket.InfoProvider.Robinhood.Contracts;
using StockMarket.InfoProvider.Robinhood.Contracts.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MA.Content.Services.StockMarketApi.Managers
{
    public interface IStockMarketQuoteManager
    {
        Task<HistoricalQuote[]> GetHistoricalQuote(HistoricalQuoteRequest request);
        Task<Quote> GetQuote(string symbol);
        Task<IEnumerable<Quote>> GetQuotes(List<string> request);
    }
}