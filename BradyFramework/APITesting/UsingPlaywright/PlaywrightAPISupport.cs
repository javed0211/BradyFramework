using ChoETL;
using Microsoft.Playwright;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradyFramework.APITesting.UsingPlaywright
{
    public class PlaywrightAPISupport
    {
        public static IPlaywright? _playwright;
        public static APIRequestContextOptions? _options;
        public static IAPIRequestContext? _request;
        public static IAPIResponse? _response;
        public static JObject? objectToRead;
        public static string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;


        public static async Task<IPlaywright> CreateAPIRequestContext()
        {
            Task<IPlaywright> _apiContext = Playwright.CreateAsync();
            _playwright = await _apiContext.ConfigureAwait(false);
            return _playwright;
        }

        public static async Task<IAPIRequestContext> APIRequestContext()
        {
            var data = File.ReadAllText(filePath + "\\APIConfig.json");
            var jsonData = System.Text.Json.JsonSerializer.Deserialize<APIConfig>(data);
            _request = await _playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions()
            {
                BaseURL = jsonData.BaseURL,
                ExtraHTTPHeaders = new Dictionary<string, string>()
                                        {
                                            {"Content-type", jsonData.Header.ContentType },
                                            {"Accept",jsonData.Header.Accept}

                                        },
                IgnoreHTTPSErrors = true,
            });
            return _request;
        }

        public static async Task<IAPIResponse> POST(string URL, APIRequestContextOptions contextOptions)
        {
            return await _request.PostAsync(URL, contextOptions);
        }

    }
}
