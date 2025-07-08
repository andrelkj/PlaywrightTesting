using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class Authentication
{
    /*
     * Authentication:
     * Once authenticated, it is possible to store the state in the browser context so you can bypass any further login attempts
     *
     * Steps required:
     * 1. Create a new browsers context providing the http credentials with the login credentials
     * 2. Store this authenticated context in a variable
     * 3. Build pages from the authenticated browsers context whenever you want these credentials to be applied
     *
     * Note: the initial browser context can still be used for building pages, but it won't contain the authenticate state
     */
    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

        // Provide credentials to initialize the browser with an authenticated context
        var authContext = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            HttpCredentials = new HttpCredentials { Username = "admin", Password = "admin" }
        });

        // var page = await browser.NewPageAsync(); // use the standard browser context to build the page
        var page = await authContext
            .NewPageAsync(); // use the authenticated context instead of the standard browser context to build the page

        // Navigation
        await page.GotoAsync("https://the-internet.herokuapp.com/basic_auth");
        await Task.Delay(2000);
    }
}