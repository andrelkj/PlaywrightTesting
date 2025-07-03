using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace PlaywrightTesting.Testcases;

public class MouseOver
{
    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation

        await page.GotoAsync("https://www.way2automation.com",
            new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

        // Actions
        if (await page.IsVisibleAsync(".eicon-close"))
        {
            await page.Locator(".eicon-close").ClickAsync();
        }

        var allCoursesLink = page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "All Courses" });
        await allCoursesLink.HoverAsync();

        var devOpsLink = page.Locator(".sub-menu")
            .GetByRole(AriaRole.Link, new LocatorGetByRoleOptions { Name = "DevOps" });
        await devOpsLink.ClickAsync();

        await Task.Delay(2000);

        await Expect(page).ToHaveTitleAsync("DevOps - Way2Automation");
        Console.WriteLine("Redirection validation passed! ✅");
    }
}