using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightTesting.Testcases;

public class Checkboxes
{
    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("http://www.tizag.com/htmlT/htmlcheckboxes.php");

        // Defining variables
        // Identify all the checkbox elements from one specific container based on the sibling header text value
        var checkboxes =
            page.Locator("h2.specialT:has-text('HTML Checkbox Form:') + div.display input[type='checkbox']");
        
        // Actions
        Console.WriteLine($"Total number of checkboxes found: {await checkboxes.CountAsync()}");
        
        Console.WriteLine("---------------------------------------------------------------------");
        
        for (int i = 0; i < await checkboxes.CountAsync(); i++)
        {
            var checkbox = checkboxes.Nth(index: i);
            Console.WriteLine($"Checkbox {i + 1} is checked: {await checkbox.IsCheckedAsync()}");
            await checkbox.CheckAsync();

            await Task.Delay(1000);
            Console.WriteLine($"After checking - Checkbox {i + 1} is checked: {await checkbox.IsCheckedAsync()}");

            Console.WriteLine("---------------------------------------------------------------------");
        }
    }
}