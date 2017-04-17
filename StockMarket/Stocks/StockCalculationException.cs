using System;

namespace StockMarket.Stocks
{
    [Serializable]
    public class StockCalculationException : Exception 
    {
        public StockCalculationException()
        {
        }

        public StockCalculationException(string message)
            : base(message)
        {
        }

        public StockCalculationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}