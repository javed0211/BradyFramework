using Microsoft.Playwright;
using SpecFlowPlaywright;
using System.Threading.Tasks;

namespace PageObjects
{
    public class BasePage
    {
        public readonly Task<IBrowserContext> _browserContext;
        private readonly Task<ITracing> _tracing;
        public static Task<IPage> _page;

        public Task<ITracing> Tracing => _tracing;

        public BasePage(BrowserDriver browserDriver)
        {
            _browserContext = CreateBrowserContextAsync(browserDriver.Current);
            _tracing = _browserContext.ContinueWith(t => t.Result.Tracing);
            _page = CreatePageAsync(_browserContext);

        }

        private async Task<IBrowserContext> CreateBrowserContextAsync(Task<IBrowser> browser)
        {

            return await (await browser).NewContextAsync(
                new()
                {
                    RecordVideoDir = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName,"videos/")
                });
        }

        private async Task<IPage> CreatePageAsync(Task<IBrowserContext> browserContext)
        {
            return await (await browserContext).NewPageAsync();
        }

    }
}