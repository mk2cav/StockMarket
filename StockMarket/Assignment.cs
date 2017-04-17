using System;
using Common.Logging;
using StockMarket.Stocks;
using StockMarket.Trades;

namespace StockMarket
{
    class Assignment
    {
        private static readonly ILog log = LogManager.GetLogger<Assignment>();

        static void Main(string[] args)
        {
            log.Debug("Start Program");

            log.Debug("Create Stock Dictionary");
            StockService stockService = new StockService();
            stockService.LoadStockList();
            TradeService tradeService = new TradeService();

            // a) i.
            CalculateTEAStockDividendYield(stockService);

            // a) ii.
            CalculatePOPStockPERatio(stockService);

            // a) iii.
            CreateSampleALEStockTrades(stockService, tradeService);

            // a) iv.
            CalculateALEStockFMVWStockPrice(stockService, tradeService);

            // b)
            CalculateGBCEAllShareIndex(stockService);
        }

        /*
        * a) i. Example of calculating the Dividend Yield for a stock.
        * 
        * @param stockService The StockService
        */
        private static void CalculateTEAStockDividendYield(StockService stockService)
        {
            log.Debug("Calculate TEA Stock Dividend Yield");
            try
            {
                Stock TEAStock = stockService.GetStock("TEA");
                if (TEAStock != null)
                {
                    double price = 30;
                    double TEADividendYield = TEAStock.DividendYield(price);
                    log.Debug("TEA Stock P/E Ratio. " + TEADividendYield.ToString());
                }
                else
                {
                    log.Warn("Unable to find TEA stock.");
                }
            }
            catch (StockCalculationException e)
            {
                log.Warn("Unable to calculate TEA Stock Dividend Yield. " + e.Message);
            }
        }

        /*
        * a) ii. Example of calculating the P/E Ratio for a stock.
        * 
        * @param stockService The StockService
        */
        private static void CalculatePOPStockPERatio(StockService stockService)
        {
            log.Debug("Calculate POP Stock P/E Ratio");
            try
            {
                Stock POPStock = stockService.GetStock("POP");
                if (POPStock != null)
                {
                    double price = 20;
                    double POPPERatio = POPStock.PERatio(price);
                    log.Debug("POP Stock P/E Ratio = " + POPPERatio.ToString());
                }
                else
                {
                    log.Warn("Unable to find POP stock.");
                }
            }
            catch (StockCalculationException e)
            {
                log.Debug("Unable to calculate POP Stock P/E Ratio. " + e.Message);
            }
        }

        /*
        * a) iii. Record sample Trades for ALE stocks.
        * 
        * @param stockService The StockService
        * @param tradeService The TradeService
        */
        private static void CreateSampleALEStockTrades(StockService stockService, TradeService tradeService)
        {
            log.Debug("Record Sample Trades");
            Stock ALEStock = stockService.GetStock("ALE");
            if (ALEStock != null)
            {
                // a) iii.
                tradeService.RecordTrade(new Trade(DateTime.Now, 100, TradeType.Buy, 10, ALEStock));
                tradeService.RecordTrade(new Trade(DateTime.Now, 50, TradeType.Buy, 15, ALEStock));
                tradeService.RecordTrade(new Trade(DateTime.Now, 150, TradeType.Buy, 18, ALEStock));
            }
            else
            {
                log.Warn("Unable to find ALE stock.");
            }
        }

        /*
        * a) iv. Record sample Trades for ALE stocks
        * 
        * @param stockService The StockService
        * @param tradeService The TradeService
        */
        private static void CalculateALEStockFMVWStockPrice(StockService stockService, TradeService tradeService)
        {
            log.Debug("Calculate ALE Stock FMVWSP");
            Stock ALEStock = stockService.GetStock("ALE");
            if (ALEStock != null)
            {
                try
                {
                    double FMVWSP = tradeService.FifteenMinuiteVolumeWeightedStockPrice(ALEStock);
                    log.Debug("ALE Stock FMVWSP = " + FMVWSP.ToString());
                }
                catch (TradesCalculationException e)
                {
                    log.Debug("Unable to calculate ALE FMVWSP. " + e.Message);
                }
            }
            else
            {
                log.Warn("Unable to find ALE stock.");
            }
        }

       /*
       * b) Calculate the GBCE All Share Index using the geometric mean of prices for all stocks
       * 
       * @param stockService The StockService
       */
        private static void CalculateGBCEAllShareIndex(StockService stockService)
        {
            log.Debug("Calculate GBCE All Share Index");
            double GBCEAllShareIndex = stockService.CalculateGBCEAllShareIndex(stockService.GetAllStockPriceList());
            log.Debug("GBCE All Share Index = " + GBCEAllShareIndex.ToString());
        }
    }
}
