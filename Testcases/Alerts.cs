using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class Alerts
{
    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Defining listeners
        // Handles alerts and dialogs
        page.Dialog += async (_, dialog) =>
        {
            await Task.Delay(3000);
            Console.WriteLine(dialog.Message);
            await dialog.AcceptAsync();
        };
        // Note: for the dialog handler to work properly you need to define it before the actual actions and it will serve as a listener

        // Navigation
        await page.GotoAsync("https://mail.rediff.com/cgi-bin/login.cgi");

        // Actions
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log In" }).ClickAsync();
        await Task.Delay(2000);

        // Close the browser
        await page.CloseAsync();
    }
}