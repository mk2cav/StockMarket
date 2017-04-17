using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockMarket.Stocks;

namespace StockMarket.Trades.Tests
{
    [TestClass]
    public class TradeTests
    {
        [TestMethod]
        public void FifteenMinuiteVolumeWeightedStockPriceTest()
        {
            double lastDividend = 20;
            double expected = 15;
            double parValue = 100;
            string TEASymbol = "TEA";
            string GINSymbol = "GIN";

            Stock TEAStock = new Stock(TEASymbol, StockType.Common, lastDividend, parValue);
            Stock GINStock = new Stock(GINSymbol, StockType.Common, lastDividend, parValue);

            TradeService tradeService = new TradeService();
            tradeService.RecordTrade(new Trade(DateTime.Now.AddMinutes(-16), 100, TradeType.Buy, 10, TEAStock));
            tradeService.RecordTrade(new Trade(DateTime.Now, 100, TradeType.Buy, 10, GINStock));
            tradeService.RecordTrade(new Trade(DateTime.Now, 100, TradeType.Buy, 10, TEAStock));
            tradeService.RecordTrade(new Trade(DateTime.Now, 300, TradeType.Buy, 20, TEAStock));
            tradeService.RecordTrade(new Trade(DateTime.Now, 200, TradeType.Buy, 10, TEAStock));

            double actual = tradeService.FifteenMinuiteVolumeWeightedStockPrice(TEAStock);

            // assert   
            Assert.AreEqual(expected, actual, "Fifteen Minuite Volume Weighted Stock Price Test calcuated incorrectly");
        }

        [TestMethod]
        public void FifteenMinuiteVolumeWeightedStockPriceDivideByZeroTest()
        {
            double lastDividend = 20;
            double parValue = 100;
            string TEASymbol = "TEA";
            string GINSymbol = "GIN";

            Stock TEAStock = new Stock(TEASymbol, StockType.Common, lastDividend, parValue);
            Stock GINStock = new Stock(GINSymbol, StockType.Common, lastDividend, parValue);

            TradeService tradeService = new TradeService();
            tradeService.RecordTrade(new Trade(DateTime.Now.AddMinutes(-16), 100, TradeType.Buy, 10, TEAStock));
            tradeService.RecordTrade(new Trade(DateTime.Now, 100, TradeType.Buy, 10, GINStock));
            tradeService.RecordTrade(new Trade(DateTime.Now, 0, TradeType.Buy, 10, TEAStock));

            // act
            try
            {
                double actual = tradeService.FifteenMinuiteVolumeWeightedStockPrice(TEAStock);
            }
            catch (TradesCalculationException e)
            {
                // assert  
                StringAssert.Contains(e.Message, TradeService.NoQuantityMessageMessage);
                return;
            }

            // assert   
            Assert.Fail("No exception was thrown.");
        }
    }
}
