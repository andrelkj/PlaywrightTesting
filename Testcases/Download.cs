using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class Download
{
    private static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Navigation
        await page.GotoAsync("https://www.selenium.dev/downloads/");

        // Actions
        var download = await page.RunAndWaitForDownloadAsync(async () =>
        {
            var seleniumLtsLink = page.GetByRole(AriaRole.Paragraph)
                .Filter(new LocatorFilterOptions { HasText = "Latest stable version " }).GetByRole(AriaRole.Link);

            await seleniumLtsLink.ClickAsync();
        });

        // Define the path where the file will be saved
        var projectDirectory = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent}/Downloads";

        // Specify the path and the file name
        var filePath = Path.Combine(projectDirectory, "selenium-server-standalone-4.1.2.jar");

        // Save the downloaded file on the given path with the specified name
        await download.SaveAsAsync(filePath);
        Console.WriteLine($"File saved at: {filePath}");
    }
}