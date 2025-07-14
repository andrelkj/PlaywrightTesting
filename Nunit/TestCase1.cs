using NUnit.Framework;

namespace PlaywrightTesting.Nunit;

public class TestCase1
{
    [Test]
    public void DoLogin()
    {
        Console.WriteLine("Executing login test case");
    }
    
    [Test]
    public void RegisterUser()
    {
        Console.WriteLine("Executing user registry test case");
    }
}