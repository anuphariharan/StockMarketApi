namespace StockMarket.InfoProvider.Robinhood.Contracts.Internal
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string MfaType { get; set; }
        public bool MfaRequired { get; set; }
    }
}