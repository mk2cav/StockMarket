using Common.Logging;

namespace StockMarket.Stocks
{
    public class Stock
    {
        private static readonly ILog log = LogManager.GetLogger<Stock>();

        public const string PriceZeroMessage = "Ticker Price cannot be Zero";
        public const string LastDividendZeroMessage = "Last Dividend cannot be Zero";

        public string Symbol { get; set; }
        public StockType Type { get; set; }
        public double LastDividend { get; set; }
        public double FixedDividend { get; set; }
        public double ParValue { get; set; }

        // Initialise the stock object
        public Stock(string symbol, StockType type, double lastDividend, double parValue, double fixedDividend = 0)
        {
            this.Symbol = symbol;
            this.Type = type;
            this.LastDividend = lastDividend;
            this.FixedDividend = parValue;
            this.ParValue = fixedDividend;
        }

        /*
	    * Calculate the dividend yield of a stock based on the Stock price
	    * 
	    * @param price The Stock price
	    * @return The Dividend Yield
	    */
        public double DividendYield(double price)
        {
            if (price == 0)
            {
                log.Debug("Dividend Yield cannot be calculated, Ticker Price cannot be Zero");
                throw new StockCalculationException(PriceZeroMessage);
            }

            if (this.Type == StockType.Common)
            {
                return this.LastDividend / price;
            }
            else
            {
                return (this.FixedDividend * (this.ParValue/100)) / price;
            }
        }

        /*
	    * Calculate the P/E Ratio of a stock based on the stock price
	    * 
	    * @param price The Stock price
	    * @return The PE Ratio
	    */
        public double PERatio(double price)
        {
            if (this.LastDividend == 0)
            {
                log.Debug("P/E Ratio cannot be calculated, Last Dividend cannot be Zero");
                throw new StockCalculationException(LastDividendZeroMessage);
            }

            return price / this.LastDividend;
        }
    }
}
