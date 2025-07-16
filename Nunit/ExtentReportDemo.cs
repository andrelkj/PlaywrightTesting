using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace PlaywrightTesting.Nunit;

public class ExtentReportDemo
{
    private ExtentReports extent;
    private ExtentTest test;

    [OneTimeSetUp]
    public void BeforeAllTest()
    {
        DateTime now = DateTime.Now;
        var fileName = $"Extent_{now:yyyy-M-d_HH-mm-ss}";

        // Create the HTML reporter with specific configurations and path
        var htmlReporter =
            new ExtentSparkReporter(
                $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Reports/{fileName}.html")
            {
                Config =
                {
                    Theme = Theme.Standard,
                    DocumentTitle = "Playwright Test Suite Report",
                    ReportName = "Playwright Test Suite Report",
                    Encoding = "utf-8"
                }
            };

        // Instantiates the Extent Reporter
        extent = new ExtentReports();

        // Attach the configured reporter to the extent instance
        extent.AttachReporter(htmlReporter);

        // Add additional information
        extent.AddSystemInfo("Automation Tester", "André Kreutzer");
        extent.AddSystemInfo("Organization", "PlaywrightTesting");
        extent.AddSystemInfo("Build", "ABC-123");
    }

    [SetUp]
    public void BeforeEachTest()
    {
        // Create a test
        test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
    }

    [TearDown]
    public void AfterEachTest()
    {
        // Get the test status
        var status = TestContext.CurrentContext.Result.Outcome.Status;

        // Report the test based on the status
        // Running the test.Fail() method alone in the test case reflect the report status only, independently of the execution status
        // Running the Assert.Fail() method alone in the test case reflects the execution status only, independently of the report status

        switch (status)
        {
            case TestStatus.Passed:
                test.Pass("Successful test case");
                break;
            case TestStatus.Failed:
                test.Fail("Failed test case");
                break;
            case TestStatus.Skipped:
                test.Skip("Skipped test case");
                break;
        }
    }

    [Test]
    public void LoginTest()
    {
        test.Info("Input the username");
        test.Info("Input the password");
        test.Info("Click the login button");
    }

    [Test]
    public void UserRegistrationTest()
    {
        test.Info("Input the first name");
        test.Info("Input the last name");
        Assert.Fail("Test case failed");
    }

    [Test]
    public void ComposeEmailTest()
    {
        test.Info("Fill required information");
        Assert.Ignore("Passed test case with Nunit");
    }

    [OneTimeTearDown]
    public void AfterAllTest()
    {
        // Report cleanup
        extent.Flush();
    }
}