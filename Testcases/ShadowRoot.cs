using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace PlaywrightTesting.Testcases;

public static class ShadowRoot
{
    /*
     * Handling shadow root elements:
     * Playwright has built-in mechanisms to handle shadow-root elements so no additional work is required.
     */
    
    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("chrome://downloads/");

        // Actions
        var searchInput = page.GetByRole(AriaRole.Searchbox, new PageGetByRoleOptions { Name = "Search download history" });
        await searchInput.FillAsync("Chrome");
        await Task.Delay(2000);
        
        // Validation
        await Expect(searchInput).ToHaveValueAsync("Chrome");
    }
}