using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class Links
{
    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("https://www.wikipedia.org");

        // Defining variables
        // Chaining locators allow more specificity without losing the readability
        var otherProjects = page.GetByRole(AriaRole.Navigation, new PageGetByRoleOptions { Name = "Other projects" }); // using the role
        var links = otherProjects.Locator("a"); // this will look for the links inside the other projects navigation block only

        Console.WriteLine("---------------------------------------------------------------------");
        
        // Actions
        Console.WriteLine($"Total number of links found: {await links.CountAsync()}");
        
        Console.WriteLine("---------------------------------------------------------------------");

        for (int i = 0; i < await links.CountAsync(); i++)
        {
            var link = links.Nth(i);
            Console.WriteLine($"Link: {await link.InnerTextAsync()} has the url: {await link.GetAttributeAsync("href")}");
        }
    }   
}