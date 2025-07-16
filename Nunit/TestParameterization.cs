using NUnit.Framework;

namespace PlaywrightTesting.Nunit;

public class TestParameterization
/*
 * Test parameterization:
 * It is possible to use [TestCase()] method as a parameter to apply different sets of data to one same test method
 */
{
    [Test]
    [TestCase("trainer@wa2auatomation.com", "sfdsfdsfd")]
    [TestCase("info@wa2auatomation.com", "sfwredsfdsfd")]
    [TestCase("trainer@wa2auatomation.com", "sfdsfxcddsfd")]
    public void LoginTest(string username, string password)
    {
        Console.WriteLine($"This test case data is: {username} - {password}");
    }

    [Test, TestCaseSource("GetTestData")]
    public void UserRgistrationTest(string firstName, string lastName)
    {
        Console.WriteLine($"This user name is: {firstName} {lastName}");
    }

    public static IEnumerable<TestCaseData> GetTestData()
    {
        /*
         * Yield argument waits for all the statements to be returned before continuing
         * Yield returns a value and store it until the next value is required and it is possible to resume the execution where it was left off
         *
         * Note: values are returned only if called
         */

        yield return new TestCaseData("André", "Jesus"); // first value returned and stored
        yield return new TestCaseData("Bárbara", "Valutky"); // the second value is returned only when needed
        yield return new TestCaseData("Ayumi", "Luiza");
    }
}