using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class Locators
{
    /*
     * Playwright locators:
     *
     * 1. CSS Selectors
     * 2. XPath
     * 3. Playwright methods (Text, Role, Label, Placeholders, Test id, Alt text - images)
     * 4. Filter locators
     * 5. Chaining locators
     *
     * Note: property provided to specify elements when using playwright methods refers to --> placeholder, aria-label or label text element values
     * Important: whenever working with text be aware of dynamic and translated values, since the text reference can change depending on the location and language
     */

    private static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("https://gmail.com");

        // Locators

        // Email or phone textbox
        // var xpath = page.Locator("//*[@id=\"identifierId\"]");
        // var css = page.Locator("#identifierId");
        // var role = page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Email or phone" });
        var labelEmail =
            page.GetByLabel("Email or phone",
                new PageGetByLabelOptions
                    { Exact = true }); // the exact property looks for elements that have the specified value exactly

        // Next button
        // var text = page.GetByText("Next");
        // var role = page.GetByRole(AriaRole.Button, new() { Name = "Next" });
        // var filter = page.Locator("button", new() { HasText = "Next" }); // filter locators (only elements that have the specified text)
        var filterBtnNext =
            page.Locator("button:has-text(\"Next\")"); //  simplified version using pseudo-selector and RegEx

        // Password textbox
        var labelPassword = page.GetByLabel("Enter your password", new PageGetByLabelOptions { Exact = true });

        // Message
        var textError = page.GetByText("Wrong password. Try again or click Forgot password to reset it.");

        // Actions
        await labelEmail.FillAsync("info@way2automation.com");
        await filterBtnNext.ClickAsync();

        await labelPassword.FillAsync("1234567890", new LocatorFillOptions { Timeout = 3000 });
        await filterBtnNext.Last.ClickAsync();

        // Validation
        Console.WriteLine(await textError.InnerTextAsync());

        await Task.Delay(2000);

        // Close the browser
        await browser.CloseAsync();
    }
}