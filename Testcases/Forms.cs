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

        var submitButton = page.Locator("#main div").Filter(new() { HasText = "Example First name: Last name" })
            .GetByRole(AriaRole.Button);
        await submitButton.HoverAsync();

        await Task.Delay(1000);

        await submitButton.EvaluateAsync(
            @"(firstNameInput) => firstNameInput.style.border = '3px solid red'"); // execute javascript

        await Task.Delay(2000);

        await submitButton.ScreenshotAsync(new LocatorScreenshotOptions
            { Path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent}/screenshots/element.png" });
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent}/screenshots/fullpage.png"
        });
        // Note: by default, screenshots are saved under /PlaywrightTesting/bin/Debug/net9.0/PlaywrightTesting folder, but it is possible to move it by accessing its parents

        // Close the browser
        await page.CloseAsync();
    }
}