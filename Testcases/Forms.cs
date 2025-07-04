using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class Forms
{
    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("https://www.w3schools.com/html/html_forms.asp");

        // Actions
        var firstNameInput = page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "First name" });
        await firstNameInput.FillAsync("André");

        var lastNameInput = page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Last name" });
        await lastNameInput.FillAsync("Kreutzer");

        await Task.Delay(2000);

        // Close the browser
        await page.CloseAsync();
    }
}