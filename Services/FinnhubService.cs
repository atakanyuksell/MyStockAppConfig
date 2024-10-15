using MyStockAppConfiguration.ServiceContracts;
using System.Text.Json;

namespace MyStockAppConfiguration.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FinnhubService(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Dictionary<string,object>> GetStockPriceQuote(string stockSymbol)
        {
            using(HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage{
                    RequestUri = new Uri("https://finnhub.io/api/v1/quote?symbol=MSFT&token=cs756opr01qkeulicbi0cs756opr01qkeulicbig"),
                    Method = HttpMethod.Get
                   
                };
                
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();

                Dictionary<string, object> responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>?>(response);

                if (responseDictionary == null)
                {
                    throw new InvalidOperationException("No response from finnhub server");
                }

                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
                }

                return responseDictionary;
            }          
        }

        Task<Dictionary<string, object>?> IFinnhubService.GetCompanyProfile(string stockSymbol)
        {
            throw new NotImplementedException();
        }

    }
}
