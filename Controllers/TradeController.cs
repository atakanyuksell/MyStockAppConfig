using Microsoft.AspNetCore.Mvc;
using MyStockAppConfiguration.ServiceContracts;
using MyStockAppConfiguration.Models;

namespace MyStockAppConfiguration.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finhubService;
        
        public TradeController(IFinnhubService finhubService)
        {
             _finhubService = finhubService;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            Dictionary<string,object>? responseDictionary = await _finhubService.GetStockPriceQuote("MSFT");

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
