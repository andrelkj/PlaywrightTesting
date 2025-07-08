using Microsoft.Playwright;

namespace PlaywrightTesting.Testcases;

public class Upload
{
    /*
     * To upload files the element needs to be of type="file" --> <input type="file" name="">
     */
    static async Task Main(string[] args)
    {
        // Setup
        using var playwright = await Playwright.CreateAsync();
        await using var browser =
            await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Upload a single file
        await page.GotoAsync("https://www.way2automation.com/way2auto_jquery/registration.php#load_box");

        var chooseFileBtn = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Choose File" });
        await chooseFileBtn.SetInputFilesAsync("/Users/andre/RiderProjects/PlaywrightTesting/Screenshots/fullpage.png");
        await Task.Delay(2000);

        // Upload multiple files
        await page.GotoAsync("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_fileupload_multiple");

        var chooseFilesBtn = page.FrameLocator("#iframeResult").Locator("#myFile");
        await chooseFilesBtn.SetInputFilesAsync([
            "/Users/andre/RiderProjects/PlaywrightTesting/Screenshots/fullpage.png",
            "/Users/andre/RiderProjects/PlaywrightTesting/Screenshots/element.png"
        ]);

        await Task.Delay(2000);
    }
}