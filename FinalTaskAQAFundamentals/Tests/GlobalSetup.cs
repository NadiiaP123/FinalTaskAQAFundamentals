using log4net;
using log4net.Config;
using System.Reflection;

namespace Tests;

[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void BeforeAllTests()
    {
        var logPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "logs/test-log.txt");

        if (File.Exists(logPath))
            File.Delete(logPath);

        XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetExecutingAssembly()), new FileInfo("log4net.config"));
    }
}