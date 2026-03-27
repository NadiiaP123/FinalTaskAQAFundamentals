using Core.Config;
using Core.Utilities;
using Core.WebDriver;
using log4net;

namespace Tests;

public abstract class BaseTest
{
    protected  ILog Log => LogManager.GetLogger(GetType());

    protected void LogStart(BrowserType browser)
    {
        string? modeStr = TestContext.Parameters.Get("RunMode");

        if (!Enum.TryParse<RunMode>(modeStr, true, out var mode))
        {
            throw new InvalidOperationException($"Invalid RunMode: '{modeStr}'");
        }

        var testName = TestContext.CurrentContext.Test.Name;
        var worker = TestContext.CurrentContext.WorkerId;
        worker = worker?.Replace("NUnit.Fw.Parallel", "");
        ThreadContext.Properties["test"] = testName;
        ThreadContext.Properties["browser"] = browser;
        ThreadContext.Properties["worker"] = worker;

        Log.Info("START TEST");
        Log.Info($"RUN MODE: {mode}");
    }

    protected static void StartDriver(BrowserType browser)
    {      
        WebDriverFactory.InitDriver(browser);
    }

    protected void StopDriver()
    {
        WebDriverFactory.QuitDriver();
        Log.Info("Stop Driver\n\n");
    }

    [TearDown]
    public void TearDown()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;

        if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
        {
            Log.Info("PASSED");
        }            
        else if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            Log.Info("FAILED");
        }
        else
        {
            Log.Warn($"WARNING: Test finished with status: {status}");
        }

        StopDriver();
    }
}
