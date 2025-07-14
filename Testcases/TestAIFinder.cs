using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class TestAIFinder
{
    private static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("https://www.gmail.com");

        // Locators
        var createAccountButton = await AIElementFinder.FindByDescriptionAsync(page, "Create account");

        // Actions
        await createAccountButton.ClickAsync();
        await Task.Delay(2000);

        // Close browser
        await browser.CloseAsync();
    }
}