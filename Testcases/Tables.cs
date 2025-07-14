using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace PlaywrightTesting.Testcases;

public class Tables
{
    private static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("https://money.rediff.com/indices/nse/NIFTY-50?5rc");

        // Actions
        // Row count
        var row = page.Locator(".dataTable tbody tr");
        Console.WriteLine(await row.CountAsync());

        // Column count
        var firstColumn = row.First.Locator("td");
        Console.WriteLine(await firstColumn.CountAsync());

        // Assertions
        await Expect(firstColumn.Nth(1)).ToContainTextAsync("Nifty");
        Console.WriteLine("Table content validation passed! ✅");

        Console.WriteLine("---------------------------------------------------------------------");

        // Iterating over table lines to print their content
        var allRowsContent = await row.AllTextContentsAsync();

        foreach (var rowContent in allRowsContent) Console.WriteLine(rowContent);
    }
}