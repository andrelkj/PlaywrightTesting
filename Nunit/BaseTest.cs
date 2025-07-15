using NUnit.Framework;

namespace PlaywrightTesting.Nunit;

public class BaseTest
{
    /*
     * Base setup can be used to specify steps that needs to run only once like database connection and closure
     */
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            TestContext.Progress.WriteLine("Running before any tests");
        }
        
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            TestContext.Progress.WriteLine("Running after any tests");
        }
    }
}