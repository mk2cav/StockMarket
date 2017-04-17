using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using StockMarket.Stocks;

namespace StockMarket.Trades
{
    public class TradeService
    {
        private static readonly ILog log = LogManager.GetLogger<TradeService>();

        public const string NoTradesMessage = "No Matching trades found";
        public const string NoQuantityMessageMessage = "Quantity cannot be Zero";

        private List<Trade> trades = new List<Trade>();

        public TradeService()
        {
        }

        /*
        * Record a new Trade for a Stock
        * 
        * @param trade The trade
        */
        public void RecordTrade(Trade trade)
        {
            trades.Add(trade);
        }

        /*
        * Calculate the Volume Weighted Stock Price based on trades in past 15 minutes for a stock
        * 
        * @return The Fifteen Minuite Volume Weighted Stock Price
        */
        public double FifteenMinuiteVolumeWeightedStockPrice(Stock stock)
        {
            DateTime comparisonTime = DateTime.Now.AddMinutes(-15);
            List<Trade> matchingTrades = trades.FindAll(trade => trade.Stock == stock && trade.Timestamp >= comparisonTime);

            if (matchingTrades.Count == 0)
            {
                log.Debug("No Matching trades found");
                throw new TradesCalculationException(NoTradesMessage);
            }

            double quantity = matchingTrades.Sum(trade => trade.Quantity);
            if (quantity == 0)
            {
                log.Debug("Fifteen Minuite Volume Weighted Stock Price cannot be calculated, Quantity cannot be Zero");
                throw new TradesCalculationException(NoQuantityMessageMessage);
            }

            double tradePriceQuantity = matchingTrades.Sum(trade => (trade.Quantity * trade.Price));

            return tradePriceQuantity / quantity;
        }
    }
}
