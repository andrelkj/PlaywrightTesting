using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class Dropdowns
{
    private static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

        // Enabling video recording
        var projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent;
        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            RecordVideoDir = $"{projectDirectory}/Videos/"
        });

        // Enabling trace viewer
        await context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        var page = await context.NewPageAsync();

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
            Console.WriteLine(
                $"This is option: {await langOption.InnerTextAsync()}, which has the code: {await langOption.GetAttributeAsync("lang")}.");

        // Stop tracing record
        await context.Tracing.StopAsync(new TracingStopOptions
        {
            Path = "trace.zip" // the root destination path is kept since we'll need to run commands from the playwright.ps1 file to access the trace viewer 
        });

        await Task.Delay(2000);

        // Close the recording context
        await context.CloseAsync();
    }
}