using NUnit.Framework;

namespace PlaywrightTesting.Nunit;

public class TestCase2
{
    /*
     * Assertions are used to compare statements
     *
     * The first assertion to return false will fail the execution, ending the execution of further code
     * To run multiple assertions while allowing failures use Assert.Multiple()
     */

    [Test]
    public void ValidateTitle()
    {
        string expectedTitle = "Google.com"; // excel sheet
        string actualTitle = "Yahoo.com"; // title

        // Conditionals do not fail tests they are only able to return values but the test will pass either way
        /*if (expectedTitle != actualTitle)
        {
            Console.WriteLine("Test failed.");
        }
        else
        {
            Console.WriteLine("Test passed.");
        }*/

        // Executing multiple assertions
        Assert.Multiple(() =>
        {
            Assert.That(expectedTitle, Is.EqualTo(actualTitle), "Titles do not match");
            Assert.That(true, "Test cases with true values are considered as passed");
            Assert.That(false, "Test cases with false values are considered as failed");
            Assert.Fail("This will force a failure in the test case");
        });
    }
}