using CsvHelper;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using PageObjects;
using SpecFlowPlaywright;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BradyFramework.PageObjects.Rates;

namespace BradyFramework.PageObjects
{
    public class ExchangeRatePage : BasePage
    {
        private Interactions _interactions;
        ScenarioContext _ScenarioContext;
        public ExchangeRatePage(BrowserDriver browserDriver, ScenarioContext scenarioContext) : base(browserDriver)
        {
            _interactions = new Interactions(_page);
            _ScenarioContext = scenarioContext;
        }

        //locators for exchange rate page
        private static string txtSearch => "(//input[contains(@class,'s-search-input')])[1]";
        private static string btnUsrName => "//button[contains(@class,'user-menu-button--logged')]";
        private static string btnSignOut => "//button[contains(@data-name,'header-user-menu-sign-out')]";



        public async Task GoTo(string url) => await _interactions.GoToUrl(url);
        public async Task SetScreenSize() => await _interactions.SetScreenSize();
        public async Task SelectTab(string tabName) => await _interactions.ClickAsync($"//div[contains(text(),'{tabName}')]//parent::a[contains(@class,'tv-category-tab')]");
        public async Task SearchCurrency(string currency)
        {
            await _interactions.SendTextAsync(txtSearch, currency.ToUpper());
            await _interactions.PressKeyboardKey("Enter");
            _ScenarioContext["currency"] = currency.ToUpper();
        }

        public async Task<List<Overview>> GetLastExchangeRate(string currency)
        {
            var lstRates = new List<Overview>();
            var btnLoadMore = "//button[contains(@class,'loadButton')]";
            var eleCurrency = (await BasePage._page).QuerySelectorAllAsync($"//tr[contains(@class,'listRow')][contains(@data-rowkey,'{currency}')]");

        GetRates:
            if ((await eleCurrency).Count <= 0)
            {
                await ClickUntilFoundAsync(await BasePage._page, btnLoadMore, $"//tr[contains(@class,'listRow')][contains(@data-rowkey,'{currency}')]");
                eleCurrency = (await BasePage._page).QuerySelectorAllAsync($"//tr[contains(@class,'listRow')][contains(@data-rowkey,'{currency}')]//td");
            }

            if (((await eleCurrency)[0].InnerTextAsync().Result.Contains((string)(currency))))
            {
                lstRates.Add(new Overview
                {
                    currency = (await eleCurrency)[0].InnerTextAsync().Result,
                    source = (string)_ScenarioContext["tabName"],
                    price = (await eleCurrency)[1].InnerTextAsync().Result,
                    chgPer = (await eleCurrency)[2].InnerTextAsync().Result,
                    chg = (await eleCurrency)[3].InnerTextAsync().Result,
                    Bid = (await eleCurrency)[4].InnerTextAsync().Result,
                    Ask = (await eleCurrency)[5].InnerTextAsync().Result,
                    high = (await eleCurrency)[6].InnerTextAsync().Result,
                    low = (await eleCurrency)[7].InnerTextAsync().Result,
                });
            }
            else
            {
                goto GetRates;
            }
            return lstRates;
        }

        private async Task ClickUntilFoundAsync(IPage page, string element, string xelement)
        {
            var timeout = DateTime.Now.AddMinutes(2);
            while (true)
            {
                try
                {
                    await page.ClickAsync(element);
                    var ele = page.QuerySelectorAllAsync(xelement);
                    if ((await ele).Count > 0)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Element not found, trying again...");
                }

                if (DateTime.Now >= timeout)
                {
                    break;
                }
            }
        }



        public async Task<List<Overview>> GetLastExchangeRate(int time, string currency)
        {
            var lstRates = new List<Overview>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < TimeSpan.FromMinutes(time).TotalMilliseconds; i++)
            {
            GetRates:
                var rows = (await BasePage._page).QuerySelectorAllAsync($"//tr[contains(@class,'listRow')][contains(@data-rowkey,'{currency}')]//td").Result;
                if (rows[0].InnerTextAsync().Result.Contains(currency))
                {
                    lstRates.Add(new Overview
                    {
                        currency = rows[0].InnerTextAsync().Result,
                        source = (string)_ScenarioContext["tabName"],
                        price = rows[1].InnerTextAsync().Result,
                        chgPer = rows[2].InnerTextAsync().Result,
                        chg = rows[3].InnerTextAsync().Result,
                        Bid = rows[4].InnerTextAsync().Result,
                        Ask = rows[5].InnerTextAsync().Result,
                        high = rows[6].InnerTextAsync().Result,
                        low = rows[7].InnerTextAsync().Result,
                    });
                    if (stopwatch.Elapsed.TotalMinutes > time)
                    {
                        stopwatch.Stop();
                        break;
                    }
                    //await SearchCurrency((currency));
                }
                else
                {
                    goto GetRates;
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0:hh\\:mm\\:ss}", stopwatch.Elapsed);
            return lstRates.GroupBy(x => x.price).Select(d => d.First()).ToList();
        }

        public async Task SignOut()
        {
            await _interactions.ClickAsync(btnUsrName);
            Thread.Sleep(2000);
            await _interactions.ClickAsync(btnSignOut);
        }
    }
}
