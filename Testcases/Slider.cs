using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class Slider
{
    private static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("https://jqueryui.com/slider/",
            new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

        // Actions
        var slider = page.Locator(".demo-frame").ContentFrame.Locator("#slider > span");
        var boundingBox = await slider.BoundingBoxAsync();

        // Calculate the center point of the slider
        var startX = boundingBox.X + boundingBox.Width / 2;
        var startY = boundingBox.Y + boundingBox.Height / 2;

        // Move to the start of the slider
        await page.Mouse.MoveAsync(startX, startY);

        // Press mouse down key
        await page.Mouse.DownAsync();

        // Move to a specific point 400px from the start
        await page.Mouse.MoveAsync(startX + 400, startY);

        // Release mouse button
        await page.Mouse.UpAsync();

        await Task.Delay(2000);
    }
}