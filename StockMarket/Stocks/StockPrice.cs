namespace StockMarket.Stocks
{
    public class StockPrice
    {
        public string Symbol { get; set; }
        public double Price { get; set; }

        // Initialise the Stock Price object
        public StockPrice(string symbol, double price)
        {
            this.Symbol = symbol;
            this.Price = price;
        }
    }
}
