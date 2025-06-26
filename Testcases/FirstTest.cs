using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class FirstTest
{
    /*
     * Playwright testing:
     *
     * While using pure playwright without adding any test runner framework (NUnit, MSTest or xUnit), there is a mandatory setup required:
     * 1. Create a playwright instance to have access to playwright methods - `Playwright.CreateAsync();`
     * 2. Launch a browser instance, so you're able to access and interact with web pages - `playwright.Chromium.LaunchAsync();`
     * 3. Create an instance of a page to navigate and perform actions - `browser.NewPageAsync();`
     *
     * Note: Playwright supports chromium, firefox and webkit browser engines
     * 
     * Once that it's done you'll be able to navigate, interact and validate e2e scenarios inside web pages.
     *
     * Important: to execute each step individually, it is required to define async methods and specify a Task type to it `static async Task Main(string[] args) {}`
     */
    
    static async Task Main(string[] args) // define the method as asynchronous and specify the type as Task 
    {
        // Mandatory setup steps for pure playwright:
        // Create a playwright instance - this makes all playwright methods available
        using var playwright = await Playwright.CreateAsync(); // use the await mthod to allow each method to be executed sequentially
        
        // Launch a new browser instance - Chromium, Firefox, Webkit
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false // set to true to run the browser in headless mode,
        });
        
        /*
         * // Define a new browser context - in this case to change the standard viewport size
        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Width = 1920,
                Height = 1080,
            }
        });
        
        var page = await context.NewPageAsync(); // uses the new browser context with the changed viewport
        */
        
        // Create a new browser tab
        var page = await browser.NewPageAsync();
        // await page.SetViewportSizeAsync(1920, 1080); // set the viewport size (width, height)
        
        // Navigation and actions:
        // Navigate to a website
        await page.GotoAsync("https://playwright.dev/");
        
        await Task.Delay(2000); // wait for 2 seconds
        
        // Validate page title
        var title = await page.TitleAsync();
        Console.WriteLine(title);
        
        // Close the browser
        await browser.CloseAsync();
    }
}