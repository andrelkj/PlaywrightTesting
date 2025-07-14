using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using PlaywrightTesting.Utilities;

namespace PlaywrightTesting.Testcases;

public class KeywordDriven
{
    private static IBrowser browser;
    private static IPage page;

    public static async Task Click(string pageName, string locatorName)
    {
        await page.Locator(XMLLocatorsReader.GetLocatorValue(pageName, locatorName)).ClickAsync();
    }

    public static async Task FIll(string pageName, string locatorName, string value)
    {
        await page.Locator(XMLLocatorsReader.GetLocatorValue(pageName, locatorName)).FillAsync(value);
    }

    private static async Task Main(string[] args)
    {
        // Setup
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath($"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Resources")
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        using var playwright = await Playwright.CreateAsync();

        if (configuration["AppSettings:browser"].Equals("chrome"))
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        else if (configuration["AppSettings:browser"].Equals("firefox"))
            browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

        page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync(configuration["AppSettings:url"]);

        // Actions
        await FIll("LoginPage", "username", "trainer@way2automation.com");
        await FIll("LoginPage", "password", "sdfsdf");
        await Click("LoginPage", "loginButton");

        await Task.Delay(2000);
    }
}