using Anup.Services.StockMarketApi.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.InfoProvider.Robinhood.Contracts.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerlessApi.Controllers
{
    [Route("api/[controller]")]
    public class StockMarketQuoteController : ControllerBase
    {
        private readonly IStockMarketQuoteManager stockMarketQuoteManager;
        private const string HistoricalQuoteRequestBindString =
            "Symbol,HistoricalInterval,HistoricalSpan,HistoricalBounds";

        public StockMarketQuoteController(IStockMarketQuoteManager stockMarketQuoteManager)
        {
            this.stockMarketQuoteManager = stockMarketQuoteManager;
        }


        [Route("Historical-Quote")]
        [HttpPost]
        public async Task<ActionResult> GetHistoricalQuote([Bind(HistoricalQuoteRequestBindString)][FromBody] HistoricalQuoteRequest request)
        {
            return StatusCode(StatusCodes.Status200OK, await stockMarketQuoteManager.GetHistoricalQuote(request));
        }


        [Route("quote")]
        [HttpGet]
        public async Task<ActionResult> GetQuote(string symbol)
        {
            return StatusCode(StatusCodes.Status200OK, await stockMarketQuoteManager.GetQuote(symbol));
        }

        [Route("quotes")]
        [HttpPost]
        public async Task<ActionResult> GetQuotes([FromBody] List<string> request)
        {
            return StatusCode(StatusCodes.Status200OK, await stockMarketQuoteManager.GetQuotes(request));
        }


    }
}
