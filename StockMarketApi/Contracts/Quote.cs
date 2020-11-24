using System;

namespace StockMarket.InfoProvider.Robinhood.Contracts
{
    public class Quote
    {
        public decimal AskPrice { get; set; }
        public decimal AskSize { get; set; }
        public decimal BidPrice { get; set; }
        public decimal BidSize { get; set; }
        public decimal LastTradePrice { get; set; }
        public decimal LastExtendedHoursTradePrice { get; set; }
        public decimal PreviousClose { get; set; }
        public decimal AdjustedPreviousClose { get; set; }
        public DateTime PreviousCloseDate { get; set; }
        public string Symbol { get; set; }
        public bool TradingHalted { get; set; }
        public bool HasTraded { get; set; }
        public string LastTradePriceSource { get; set; }
        public DateTime UpdatedAt { get; set; }
        public System.Uri Instrument { get; set; }

        public override string ToString()
        {
            return $"{nameof(AskPrice)}: {AskPrice}, {nameof(AskSize)}: {AskSize}, {nameof(BidPrice)}: {BidPrice}, {nameof(BidSize)}: {BidSize}, {nameof(LastTradePrice)}: {LastTradePrice}, {nameof(LastExtendedHoursTradePrice)}: {LastExtendedHoursTradePrice}, {nameof(PreviousClose)}: {PreviousClose}, {nameof(AdjustedPreviousClose)}: {AdjustedPreviousClose}, {nameof(PreviousCloseDate)}: {PreviousCloseDate}, {nameof(Symbol)}: {Symbol}, {nameof(TradingHalted)}: {TradingHalted}, {nameof(HasTraded)}: {HasTraded}, {nameof(LastTradePriceSource)}: {LastTradePriceSource}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(Instrument)}: {Instrument}";
        }
    }
}