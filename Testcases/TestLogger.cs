using log4net;
using log4net.Config;

namespace PlaywrightTesting.Testcases;

public class TestLogger
{
    // Initialize the logger
    private static readonly ILog Log = LogManager.GetLogger(typeof(TestLogger));

    static void Main(string[] args)
    {
        // Access the repository and specify the logger path
        var loggerRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(loggerRepository,
            new FileInfo(
                $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}/Resources/log4net.config"));

        // Call log methods
        Log.Info("Test execution started!");
        Log.Debug("This is a debug message.");
        Log.Warn("This is a warning message.");
        Log.Error("This is an error message.");
        Log.Fatal("This is a fatal message.");
    }
}