using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace PlaywrightTesting.Testcases;

public class Assertions
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

        // Assertions
        await Expect(page).ToHaveURLAsync("http://www.tizag.com/htmlT/htmlcheckboxes.php");
        Console.WriteLine("Url validation passed! ✅");

        await Expect(page).Not.ToHaveURLAsync("Error");
        Console.WriteLine("Error validation passed! ✅");

        await Expect(page).ToHaveTitleAsync("HTML Tutorial - Checkboxes");
        Console.WriteLine("Title validation passed! ✅");

        var tagsLink = page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "HTML - Tags" });
        await Expect(tagsLink).ToHaveTextAsync("HTML - Tags");
        Console.WriteLine("Tags link text validation passed! ✅");

        var preSelectedSoccerCheckbox = page.Locator("input[value=soccer] ").Last;
        await Expect(preSelectedSoccerCheckbox).ToBeVisibleAsync();
        Console.WriteLine("Pre selected soccer checkbox visibility validation passed! ✅");
        
        await Expect(preSelectedSoccerCheckbox).ToBeCheckedAsync();
        Console.WriteLine("Pre selected soccer checkbox state validation passed! ✅");
    }
}