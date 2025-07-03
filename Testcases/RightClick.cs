using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace PlaywrightTesting.Testcases;

public class RightClick
{
    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("https://deluxe-menu.com/popup-mode-sample.html");

        // Actions
        await page.Locator(".contentTd img").Last.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        await Expect(page.Locator("#dm2m1")).ToBeVisibleAsync();
        Console.WriteLine("Image options visibility validation passed! ✅");

        await Task.Delay(2000);
    }
}