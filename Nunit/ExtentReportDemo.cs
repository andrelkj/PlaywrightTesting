using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace PlaywrightTesting.Nunit;

public class ExtentReportDemo
{
    private ExtentReports extent;
    private ExtentTest test;

    // Methods
    // Generates the report with the proper status and style based on the test execution status
    private void ReportTestResult(TestStatus status)
    {
        var message = $"{GetStatusDescription(status)} test case: {TestContext.CurrentContext.Result.Message}";
        var (label, color) = GetStatusLabelAndColor(status);
        var markup = MarkupHelper.CreateLabel(label, color);

        switch (status)
        {
            case TestStatus.Passed:
                test.Pass(message);
                test.Pass(markup);
                break;
            case TestStatus.Failed:
                test.Fail(message);
                test.Fail(markup);
                break;
            case TestStatus.Skipped:
                test.Skip(message);
                test.Skip(markup);
                break;
        }
    }

    // Defines a description for each test status
    private static string GetStatusDescription(TestStatus status) => status switch
    {
        TestStatus.Passed => "Successful",
        TestStatus.Failed => "Failed",
        TestStatus.Skipped => "Skipped",
        _ => "Unknown"
    };

    // Defines a label and color for each test status
    private static (string label, ExtentColor color) GetStatusLabelAndColor(TestStatus status) => status switch
    {
        TestStatus.Passed => ("PASS", ExtentColor.Green),
        TestStatus.Failed => ("FAIL", ExtentColor.Red),
        TestStatus.Skipped => ("SKIP", ExtentColor.Amber),
        _ => ("UNKNOWN", ExtentColor.Grey)
    };

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
        ReportTestResult(status);
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
        Assert.Ignore("Test case was skipped");
    }

    [OneTimeTearDown]
    public void AfterAllTest()
    {
        // Report cleanup
        extent.Flush();
    }
}