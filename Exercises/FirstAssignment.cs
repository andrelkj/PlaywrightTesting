using Microsoft.Playwright;

namespace PlaywrightTesting.Exercises;

public class FirstAssignment
{
    /*
     * Exercise 1: Counting google search results
     * 1. Navigate to `https://www.google.com`
     * 2. Search for "way2automation"
     * 3. Count the number of links from the search results page (first page only)
     * 4. Print the count of links to the console
     *
     * Exercise 2: Printing popular origin cities from MakeMyTrip website
     * 1. Navigate to `https://www.makemytrip.com`
     * 2. Locate the origin dropdown selector
     * 3. Perform a click action to open dropdown
     * 4. Print the popular cities listed
     */

    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Exercise 1
        // await page.GotoAsync("https://www.google.com");
        //
        // var search = page.GetByRole(AriaRole.Combobox, new PageGetByRoleOptions { Name = "Pesquisar" });
        // await search.FillAsync("way2automation");
        //
        // await search.PressAsync("Enter");
        //
        // var searchLinks = page.GetByRole(AriaRole.Main).GetByRole(AriaRole.Link);
        // Console.WriteLine($"Count of links from the search result: {await searchLinks.CountAsync()}");

        // Exercise 2
        await page.GotoAsync("https://www.makemytrip.global/?cc=br");

        // Close modal
        await page.Locator("section").Filter(new() { HasText = "Login now to get FLAT 12% OFF" }).Locator("span").First
            .ClickAsync();

        await page.WaitForLoadStateAsync(LoadState.Load);
        await page.Locator(".searchCity").ClickAsync();

        await page.GetByText("POPULAR CITIES").IsVisibleAsync();
        
        var popularCities = page.GetByRole(AriaRole.Listbox).GetByRole(AriaRole.Option).Locator("p");

        for (int i = 0; i < await popularCities.CountAsync(); i++)
        {
            Console.WriteLine($"City name: {await popularCities.Nth(i).InnerTextAsync()}");
        }

        await Task.Delay(2000);
        
        // Close the browser
        await browser.CloseAsync();
    }
}