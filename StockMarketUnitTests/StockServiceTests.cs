using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace StockMarket.Stocks.Tests
{
    [TestClass]
    public class StockServiceTests
    {
        [TestMethod]
        public void CalculateGBCEAllShareIndexTest()
        {
            double expected = Math.Pow(60, 1.0 / 3);

            List<StockPrice> stockPrices = new List<StockPrice>();
            stockPrices.Add(new StockPrice("TEA",10));
            stockPrices.Add(new StockPrice("POP", 20));
            stockPrices.Add(new StockPrice("ALE", 30));

            StockService stockService = new StockService();

            // act  
            double actual = stockService.CalculateGBCEAllShareIndex(stockPrices);

            // assert   
            Assert.AreEqual(expected, actual, "GBCE All Share Index calcuated incorrectly");
        }

        [TestMethod]
        public void CalculateGBCEAllShareIndexTestEmptyList()
        {
            double expected = 0;

            List<StockPrice> stockPrices = new List<StockPrice>();
            StockService stockService = new StockService();

            // act  
            double actual = stockService.CalculateGBCEAllShareIndex(stockPrices);

            // assert   
            Assert.AreEqual(expected, actual, "GBCE All Share Index empty list calcuated incorrectly");
        }
    }
}
