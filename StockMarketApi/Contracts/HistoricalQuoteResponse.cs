namespace StockMarket.InfoProvider.Robinhood.Contracts.Internal
{
    public class HistoricalQuoteResponse
    {
        public System.Uri Quote { get; set; }
        public string Symbol { get; set; }
        public System.Uri Instrument { get; set; }
        public HistoricalQuote[] Historicals { get; set; }
    }
}