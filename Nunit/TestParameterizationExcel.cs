using NUnit.Framework;
using PlayWrightTesting.utilities;

namespace PlaywrightTesting.Nunit
{
    internal class TestParameterizationExcel
    {
        [Test, TestCaseSource("GetTestData")]
        public void LoginTest(string username, string password)
        {
            Console.WriteLine($"This test case data is: {username} - {password}");
        }


        public static IEnumerable<TestCaseData> GetTestData()
        {
            var columns = new List<string> { "username", "password" };

            return DataUtil.GetTestDataFromExcel(
                $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Resources/testData.xlsx",
                "LoginTest", columns);
        }


        [Test, TestCaseSource("GetUserRegTestData")]
        public void UserRegTest(string firstName, string lastName)
        {
            Console.WriteLine($"This user name is: {firstName} {lastName}");
        }

        public static IEnumerable<TestCaseData> GetUserRegTestData()
        {
            var columns = new List<string> { "firstname", "lastname" };

            return DataUtil.GetTestDataFromExcel(
                $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Resources/testData.xlsx",
                "UserRegTest", columns);
        }
    }
}