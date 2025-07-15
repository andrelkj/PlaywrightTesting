using NUnit.Framework;

namespace PlaywrightTesting.Nunit;

public class RetryTest
{
    private static int _counter;

    [Test, Retry(3)]
    public void TestMethod_withRetries()
    {
        _counter++;

        if (_counter < 3)
        {
            TestContext.Progress.WriteLine($"Test attempt: {_counter}"); // prompt the message during the test execution
            Assert.Fail("Simulated failure");
        }

        // Pass on the 3rd attempt
        Assert.Pass($"Test passed after {_counter} attempts");
    }
}