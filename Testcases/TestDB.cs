using PlayWrightTesting.utilities;

namespace PlaywrightTesting.Testcases;

public class TestDB
{
    static void Main(string[] args)
    {
        DBManager.SetMySQLDBConnection();
        var results = DBManager.GetQuery("select tutorial_author from selenium");
        Console.WriteLine(string.Join(", ", results));
    }
}