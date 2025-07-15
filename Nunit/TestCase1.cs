using NUnit.Framework;

namespace PlaywrightTesting.Nunit;

public class TestCase1
{
    [SetUp] // Executed before each test case
    public void LaunchBrowser()
    {
        Console.WriteLine("Launching browser");
    }

    [TearDown] // Execute after each test case
    public void CloseBrowser()
    {
        Console.WriteLine("Closing browser");   
    }

    [Test, Order(1), Category("smoke")] // Defines a test case of order 1 and type smoke
    public void RegisterUser()
    {
        Console.WriteLine("Executing user registry test case");
    }

    [Test, Order(2), Category("smoke")] // Defines a test case of order 2 - will be executed after order 1
    public void DoLogin()
    {
        Console.WriteLine("Executing login test case");
    }
    
    [Test, Order(3), Category("bvt"), Ignore("Skip test case 3")] // Defines a test case of order 3 - will be executed after order 2
    public void Test3()
    {
        Console.WriteLine("Executing test case 3");
    }
    
    [Test, Order(4), Category("functional")] // Defines a test case of order 4 - will be executed after order 3
    public void Test4()
    {
        Console.WriteLine("Executing test case 4");
    }
    
    [Test, Order(5), Category("functional")] // Defines a test case of order 5 - will be executed after order 4
    public void Test5()
    {
        Console.WriteLine("Executing test case 5");
    }
}