using StockMarket.InfoProvider.Robinhood.Contracts.Enum;

namespace StockMarket.InfoProvider.Robinhood.Contracts.Requests
{
    public class HistoricalQuoteRequest
    {
        public string Symbol { get; set; }
        public HistoricalInterval Interval { get; set; }
        public HistoricalSpan Span { get; set; }
        public HistoricalBounds Bounds { get; set; }
    }
}