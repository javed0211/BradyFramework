using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using PageObjects;
using SpecFlowPlaywright;
using BoDi;
using BradyFramework.PageObjects;
using Allure.Commons;

namespace Hooks
{
    /// <summary>
    /// Calculator related hooks
    /// </summary>
    [Binding]
    public class Hooks
    {
        private readonly string _traceName;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _traceName = scenarioContext.ScenarioInfo.Title.Replace(" ", "_");
            _scenarioContext = scenarioContext;
        }


        [AfterScenario(Order = 1)]
        public async void AttachScreenShot()
        {
            if (_scenarioContext.TestError != null)
            {
                try
                {
                    var fileName = _scenarioContext.ScenarioInfo.Title + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                    var filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Screenshots" + fileName;
                    await TakeScreenshot(filePath);

                    if (!Directory.Exists(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Screenshots"))
                        Directory.CreateDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Screenshots");

                    AllureLifecycle.Instance.AddAttachment(fileName, "image/png", filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Take screenshot of current page
        /// </summary>
        /// <returns></returns>
        public async Task TakeScreenshot(string Path)
        {
            await (await BasePage._page).ScreenshotAsync(
                                new PageScreenshotOptions
                                {
                                    Path = Path,
                                    Type = ScreenshotType.Png
                                });
        }

        [AfterScenario("UI")]
        public async Task CloseContext()
        {
            await (await BasePage._page).CloseAsync();
        }


    }
}