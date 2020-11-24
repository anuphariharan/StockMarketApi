using System;

namespace StockMarket.InfoProvider.Robinhood.Contracts
{
    public class HistoricalQuote
    {
        public DateTime BeginsAt { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public long Volume { get; set; }
        public string Session { get; set; }
        public bool Interpolated { get; set; }

        public override string ToString()
        {
            return $"{nameof(BeginsAt)}: {BeginsAt}, {nameof(OpenPrice)}: {OpenPrice}, {nameof(ClosePrice)}: {ClosePrice}, {nameof(HighPrice)}: {HighPrice}, {nameof(LowPrice)}: {LowPrice}, {nameof(Volume)}: {Volume}, {nameof(Session)}: {Session}, {nameof(Interpolated)}: {Interpolated}";
        }
    }
}