using BradyFramework.PageObjects;
using ChoETL;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using SpecFlowPlaywright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BradyFramework.PageObjects.Rates;

namespace BradyFramework.Steps
{
    [Binding]
    public sealed class ExchangeRatePageSteps
    {
        private readonly ExchangeRatePage _exchangeRatePage;
        ScenarioContext _scenarioContext;
        public ExchangeRatePageSteps(ExchangeRatePage ratePage,ScenarioContext scenarioContext)
        {
            _exchangeRatePage = ratePage;
            _scenarioContext = scenarioContext;
        }


        [Given(@"I navigate to TradingView exchange rates")]
        public async Task GivenINavigateToTradingViewExchangeRates()
        {
            await _exchangeRatePage.GoTo("https://www.tradingview.com/markets/currencies/rates-all/");
            await _exchangeRatePage.SetScreenSize();
        }

        [When(@"I choose '([^']*)' tab")]
        public async Task WhenIChooseTab(string tabName)
        {
            await _exchangeRatePage.SelectTab(tabName);
            _scenarioContext["tabName"] = tabName;
        }

        [When(@"I find last exchange rate for currency pair '([^']*)'")]
        public async Task WhenIFindLastExchangeRateForCurrencyPair(string currency)
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Results";
            await _exchangeRatePage.SearchCurrency(currency);
            var rates = await _exchangeRatePage.GetLastExchangeRate();
            using (var rate = new ChoCSVWriter<Overview>(Path.Combine(dir, $"Rates-{(string)_scenarioContext["tabName"]}.csv")).WithFirstLineHeader())
            { rate.Write(rates); }
        }

        [When(@"I find last '([^']*)' exchange rates for '([^']*)' for a period of '([^']*)' minute")]
        public async Task WhenIFindLastExchangeRatesForForAPeriodOfMinute(string noOfInstance, string currency, int time)
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Results";
            await _exchangeRatePage.SearchCurrency(currency);
            var rates = await _exchangeRatePage.GetLastExchangeRate(time,currency);
            using (var rate = new ChoCSVWriter<Overview>(Path.Combine(dir, $"Rates-{(string)_scenarioContext["tabName"]}-1min.csv")).WithFirstLineHeader())
            { rate.Write(rates); }
        }

        [When(@"I sign out")]
        public async Task WhenISignOut()
        {
            await _exchangeRatePage.SignOut();
        }


    }
}
