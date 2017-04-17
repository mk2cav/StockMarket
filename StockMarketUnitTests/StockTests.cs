using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StockMarket.Stocks.Tests
{
    [TestClass]
    public class StockTests
    {
        [TestMethod]
        public void DividendYieldCommonStockTypeTest()
        {
            double lastDividend = 20;
            double price = 2.5;
            double expected = 8;
            double parValue = 100;
            string symbol = "TEA";

            Stock TEAStock = new Stock(symbol, StockType.Common, lastDividend, parValue);

            // act  
            double actual = TEAStock.DividendYield(price);

            // assert   
            Assert.AreEqual(expected, actual, "Common Stock Dividend Yield calcuated incorrectly");
        }

        [TestMethod]
        public void DividendYieldPreferredStockTypeTest()
        {
            double lastDividend = 8;
            double fixedDividend = 2;
            double price = 2.5;
            double parValue = 100;
            string symbol = "GIN";
            double expected = 0.8;
            
            Stock GINStock = new Stock(symbol, StockType.Preferred, lastDividend, parValue, fixedDividend);

            // act  
            double actual = GINStock.DividendYield(price);

            // assert  
            Assert.AreEqual(expected, actual, "Preferred Stock Dividend Yield calcuated incorrectly");
        }

        [TestMethod]
        public void DividendYieldDivideByZeroTest()
        {
            double lastDividend = 8;
            double fixedDividend = 0.02;
            double price = 0;
            double parValue = 100;
            string symbol = "GIN";

            Stock GINStock = new Stock(symbol, StockType.Preferred, lastDividend, parValue, fixedDividend);

            // act
            try
            {
                double actual = GINStock.DividendYield(price); ;
            }
            catch (StockCalculationException e)
            {
                // assert  
                StringAssert.Contains(e.Message, Stock.PriceZeroMessage);
                return;
            }

            // assert
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        public void PERatioStockTest()
        {
            double lastDividend = 8;
            double fixedDividend = 0.02;
            double price = 2.5;
            double parValue = 100;
            string symbol = "GIN";
            double expected = 0.3125;

            Stock GINStock = new Stock(symbol, StockType.Preferred, lastDividend, parValue, fixedDividend);

            // act  
            double actual = GINStock.PERatio(price);

            // assert  
            Assert.AreEqual(expected, actual, "P/E Ratio calcuated incorrectly");
        }

        [TestMethod]
        public void PERatioDivideByZeroTest()
        {
            double lastDividend = 0;
            double fixedDividend = 0.02;
            double price = 10;
            double parValue = 100;
            string symbol = "GIN";

            Stock GINStock = new Stock(symbol, StockType.Preferred, lastDividend, parValue, fixedDividend);

            // act
            try
            {
                double actual = GINStock.PERatio(price);
            }
            catch (StockCalculationException e)
            {
                // assert  
                StringAssert.Contains(e.Message, Stock.LastDividendZeroMessage);
                return;
            }

            // assert
            Assert.Fail("No exception was thrown.");
        }
    }
}
