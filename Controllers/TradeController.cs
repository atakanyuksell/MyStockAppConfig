using Microsoft.AspNetCore.Mvc;
using MyStockAppConfiguration.ServiceContracts;
using MyStockAppConfiguration.Models;
using Microsoft.Extensions.Options;
using MyStockAppConfiguration;

namespace MyStockAppConfiguration.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finhubService;
        private readonly IOptions<TradingOptions> _tradingOption;

        public TradeController(IFinnhubService finhubService,IOptions<TradingOptions> tradingOption)
        {
             _finhubService = finhubService;
            _tradingOption = tradingOption;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOption.Value.DefaultStockSymbol == null)
            {
                _tradingOption.Value.DefaultStockSymbol = "MSFT";
            }
            Dictionary<string,object>? responseDictionary = await _finhubService.GetStockPriceQuote(_tradingOption.Value.DefaultStockSymbol);

            StockTrade stock = new StockTrade()
            {
                //StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
                StockName = responseDictionary["c"].ToString(),
                Price = Convert.ToDouble(responseDictionary["h"].ToString()),
                
                //LowestPrie = Convert.ToDouble(responseDictionary["l"].ToString()),
                //OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString())
            };


            return View(stock);
        }
     
    }
}
