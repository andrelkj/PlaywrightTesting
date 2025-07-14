using Microsoft.Extensions.Configuration;

namespace PlaywrightTesting.Utilities;

public class ReadingAppSettings
{
    private static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath($"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Resources")
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var browser = configuration["AppSettings:browser"];
        var testsiteurl = configuration["AppSettings:testsiteurl"];
        var env = configuration["AppSettings:env"];

        Console.WriteLine(browser);
        Console.WriteLine(testsiteurl);
        Console.WriteLine(env);
    }
}