using NUnit.Framework;
using PlayWrightTesting.utilities;

namespace PlaywrightTesting.Nunit;

[Parallelizable]
public class ParallelTesting
{
    [Parallelizable(ParallelScope.Children)]
    [Test, TestCaseSource("GetTestData")]
    public void LoginTest(string username, string password)
    {
        DateTime now = DateTime.Now;
        var time = $"{now:yyyy-M-d_HH-mm-ss}";

        Console.WriteLine($"This test case data is: {username} - {password}, and was execute at: {time}");
        Thread.Sleep(1000);
    }


    public static IEnumerable<TestCaseData> GetTestData()
    {
        var columns = new List<string> { "username", "password" };

        return DataUtil.GetTestDataFromExcel(
            $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Resources/testData.xlsx",
            "LoginTest", columns);
    }

    [Parallelizable(ParallelScope.Children)]
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
    }
}