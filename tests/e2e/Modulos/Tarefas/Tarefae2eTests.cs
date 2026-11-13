using Microsoft.Playwright;

namespace Tarefae2eTests;

[TestClass]
public sealed class Tarefae2eTests
{
    [TestMethod]
    public async Task TestMethod1()
    {
        IPlaywright playwright = await Playwright.CreateAsync();
        IBrowser browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        IBrowserContext browserContext = await browser.NewContextAsync();

        IPage page = await browserContext.NewPageAsync();

        await page.GotoAsync("https://chatgpt.com/");

        await page.GetByText("O que você pode fazer?").ClickAsync();

        await browser.DisposeAsync();
        playwright.Dispose();
    }
}
