using Common.Logging;
using System.Collections.Generic;
using System;
using System.Linq;

namespace StockMarket.Stocks
{
    public class StockService
    {
        private static readonly ILog log = LogManager.GetLogger<StockService>();
        private Dictionary<string, Stock> stocks = new Dictionary<string, Stock>();

        public StockService()
        {
        }

        // Initialise the stocks list
        public void LoadStockList()
        {
            stocks.Add("TEA", new Stock("TEA", StockType.Common, 0, 100));
            stocks.Add("POP", new Stock("POP", StockType.Common, 8, 100));
            stocks.Add("ALE", new Stock("ALE", StockType.Common, 23, 60));
            stocks.Add("GIN", new Stock("GIN", StockType.Common, 8, 100, 2));
            stocks.Add("JOE", new Stock("JOE", StockType.Common, 13, 250));
        }

        /*
        * Get a Stock object for a Stock Symbol
        * 
        * @param symbol The Stock Symbol
        * @return The Stock
        */
        public Stock GetStock(string symbol)
        {
            Stock stock;
            return this.stocks.TryGetValue(symbol, out stock) ? stock : null;
        }

        /*
        * Get a Stock price for a Stock Symbol
        * 
        * @param symbol The Stock Symbol
        * @return The Stock
        */
        public double GetStockPrice(string symbol)
        {
            // Return random number for stock price, assumed that true implementation will use a price service to retreive current stock prices.
            Random rnd = new Random();
            return rnd.Next(1, 1000);
        }

        /*
        * Get a Stock price list for all Stocks
        * 
         * @return The StockPrice List
        */
        public List<StockPrice> GetAllStockPriceList()
        {
            List<StockPrice> stockPrices = new List<StockPrice>();
            foreach(KeyValuePair<string, Stock> keyValuePair in this.stocks)
            {
                stockPrices.Add(new StockPrice(keyValuePair.Value.Symbol, GetStockPrice(keyValuePair.Value.Symbol)));
            }

            return stockPrices;
        }

        /*
        * Get a Stock price for a Stock Symbol
        * 
        * @param symbol The Stock Symbol
        * @return The Stock
        */
        public double CalculateGBCEAllShareIndex(List<StockPrice> stockPrices)
        {
            return Math.Pow(stockPrices.Sum(stockPrice => stockPrice.Price), 1.0 / stockPrices.Count);
        }        
    }
}
