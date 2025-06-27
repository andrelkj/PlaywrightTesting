using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class Dropdowns
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
        
        // Actions
        // Select the language
        // await page.SelectOptionAsync("select", "English"); // using the element text
        // await page.SelectOptionAsync("select", new [] { "Português"});
        // await page.SelectOptionAsync("select", "hi"); // using the element value
        await page.SelectOptionAsync("select", new SelectOptionValue { Index = 1 }); // select by index
        await Task.Delay(1000);
        await page.SelectOptionAsync("select", new SelectOptionValue { Value = "hi" }); // select by index
        await Task.Delay(1000);
        await page.SelectOptionAsync("select", new SelectOptionValue { Label = "Eesti" }); // select by index
        
        // Counting elements - playwright allows handling multiple elements with the same locator
        Console.WriteLine("Interacting with multiple elements with the same locator:");
        
        var langOptions = await page.QuerySelectorAllAsync("select > option");
        
        Console.WriteLine("---------------------------------------------------------------------");
        
        Console.WriteLine($"Total count of available languages is: {langOptions.Count}");
        
        Console.WriteLine("---------------------------------------------------------------------");

        // Iterating over all the language options
        Console.WriteLine("Iterating over all the language options:");
        
        foreach (var langOption in langOptions)
        {
            Console.WriteLine($"This is option: { await langOption.InnerTextAsync()}, which has the code: { await langOption.GetAttributeAsync("lang")}.");
        }
        
        // Close the browser
        await browser.CloseAsync();

        await Task.Delay(2000);
    }
}