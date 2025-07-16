using Microsoft.Playwright;
using NUnit.Framework;
using PlayWrightTesting.utilities;

namespace PlaywrightTesting.Nunit;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class ParallelTesting
{
    private IPlaywright playwright;

    [SetUp]
    public async Task Setup()
    {
        playwright = await Playwright.CreateAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        playwright.Dispose();
    }

    [Parallelizable(ParallelScope.Children)]
    [Test, TestCaseSource("GetTestData")]
    public async Task LoginTest(string username, string password, string browserType)
    {
        DateTime now = DateTime.Now;
        var time = $"{now:yyyy-M-d_HH-mm-ss}";

        Console.WriteLine($"This test case data is: {username} - {password}, and was execute at: {time}");
        Thread.Sleep(1000);

        IBrowser browser;

        switch (browserType)
        {
            case "chrome":
                browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
                break;
            case "firefox":
                browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
                break;
            case "webkit":
                browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }); break;
            default:
                Assert.Fail($"Browser type '{browserType}' is not supported");
                return;
        }

        IPage page = await browser.NewPageAsync();

        await page.GotoAsync("https://www.facebook.com");
        Console.WriteLine(await page.TitleAsync());

        await page.Locator("#email").FillAsync(username);
        await page.Locator("#pass").FillAsync(password);

        Thread.Sleep(2000);
    }

    public static IEnumerable<TestCaseData> GetTestData()
    {
        var columns = new List<string> { "username", "password", "browser" };

        return DataUtil.GetTestDataFromExcel(
            $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Resources/testData.xlsx",
            "LoginTest", columns);
    }

    /*[Parallelizable(ParallelScope.Children)]
    [Test, TestCaseSource("GetUserRegTestData")]
    public void UserRegTest(string firstName, string lastName)
    {
        DateTime now = DateTime.Now;
        var time = $"{now:yyyy-M-d_HH-mm-ss}";

        Console.WriteLine($"This user name is: {firstName} {lastName}, and was execute at: {time}");
        Thread.Sleep(1000);
    }

    public static IEnumerable<TestCaseData> GetUserRegTestData()
    {
        var columns = new List<string> { "firstname", "lastname" };

        return DataUtil.GetTestDataFromExcel(
            $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Resources/testData.xlsx",
            "UserRegTest", columns);
    }*/
}