using Microsoft.Playwright;

namespace PlaywrightTesting.testcases;

internal class TestAIImageComparison
{
    private static async Task Main(string[] args)
    {
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();
        await page.SetViewportSizeAsync(1920, 1080);
        await page.GotoAsync("https://gmail.com");


        await page.ScreenshotAsync(new PageScreenshotOptions
            { Path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\screenshot\\gmail.png" });

        await Task.Delay(5000);

        var match = AImageElementFinder.FindElementByImage(
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\screenshot\\gmail.png",
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\screenshot\\createaccount.png");

        if (match != null)
            await page.Mouse.ClickAsync(match.Value.X, match.Value.Y);
        else
            Console.WriteLine("Element not found");


        await Task.Delay(5000);
    }
}