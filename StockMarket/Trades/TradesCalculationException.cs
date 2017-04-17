using System;

namespace StockMarket.Trades
{
    [Serializable]
    public class TradesCalculationException : Exception
    {
        public TradesCalculationException()
        {
        }

        public TradesCalculationException(string message)
            : base(message)
        {
        }

        public TradesCalculationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}