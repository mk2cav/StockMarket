using System;
using Common.Logging;
using StockMarket.Stocks;

namespace StockMarket.Trades
{
    public class Trade
    {
        private static readonly ILog log = LogManager.GetLogger<Trade>();

        public DateTime Timestamp { get; set; }
        public int Quantity { get; set; }
        public TradeType Type { get; set; }
        public double Price { get; set; }
        public Stock Stock { get; set; }

        // Initialise the trade object
        public Trade(DateTime timestamp, int quantity, TradeType type, double price, Stock stock)
        {
            this.Timestamp = timestamp;
            this.Quantity = quantity;
            this.Type = type;
            this.Price = price;
            this.Stock = stock;
        }
    }
}
