using BradyFramework.APITesting;
using BradyFramework.APITesting.UsingPlaywright;
using BradyFramework.APITesting.UsingRestSharp;
using BradyFramework.PageObjects;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BradyFramework.Steps
{
    [Binding]
    public sealed class APITestSteps
    {
        //private readonly PlaywrightAPISupport _playwrightAPI;
        ScenarioContext _scenarioContext;
        public APITestSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I call POST employee API '([^']*)'")]
        public async Task GivenICallPOSTEmployeeAPI(string endpoint)
        {
            await PlaywrightAPISupport.CreateAPIRequestContext();
            var request = await PlaywrightAPISupport.APIRequestContext();
            var data = new Dictionary<string, string>();
            data.Add("body", @"{""name"":""test"",""salary"":""123"",""age"":""23""}");
            var response = await request.PostAsync(endpoint, new() { DataObject = data });
            if (response.Status != 200)
                Console.WriteLine(response.StatusText);
            else
                Console.WriteLine(response.JsonAsync().Result);
        }

        [Given(@"I call GET employee API '([^']*)'")]
        public void GivenICallGETEmployeeAPI(string endPoint)
        {
            var data = File.ReadAllText(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName + "\\APIConfig.json");
            var jsonData = System.Text.Json.JsonSerializer.Deserialize<APIConfig>(data);
            var apiClient = new RestSharpAPISupport();
            var response = apiClient.GetAPI(jsonData.BaseURL, endPoint);
            if (response.IsSuccessStatusCode)
                Console.WriteLine(response.Content);
            else
                Console.WriteLine($"Error: {response.StatusCode}");

        }

    }
}
