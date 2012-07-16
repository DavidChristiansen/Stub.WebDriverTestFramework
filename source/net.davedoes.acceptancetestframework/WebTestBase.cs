using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Common.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using net.davedoes.acceptancetestframework.Local;

namespace net.davedoes.acceptancetestframework {
    public abstract class WebTestBase {
        private Action deleteCommand;
        protected static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        protected string baseURL;
        protected enum BrowserTypes {
            All,
            FireFox,
            IE,
            Chrome
        }

        protected virtual void InitTest() {

        }

        protected virtual void TestScript(Action<IWebDriver> action, params BrowserTypes[] browserTypes)
        {
            var allTestsPassed = false;
            foreach (IEnumerable<WebTester> testEngine in browserTypes.Select(GetTester)) {
                var testPassed = testEngine.All(tester => {
                    Exception caughtException = null;
                    try {
                        Logger.InfoFormat("Running Test on {0} ::", tester.GetType().FullName);
                        tester.Run(action);
                    } catch (Exception ex) {
                        caughtException = ex;
                    } finally {
                        tester.ShutDown();
                    }
                    if (caughtException == null)
                        return true;
                    return false;
                });
                allTestsPassed = testPassed;
                if (!testPassed)
                    break;
            }
            AssertTrue(allTestsPassed, "All tests should pass - numbchuck!");
        }

        protected abstract void AssertTrue(bool boolToAssert, string message);
        protected abstract void AssertFalse(bool boolToAssert, string message);

        protected IEnumerable<DesiredCapabilities> GetRemoteTester(BrowserTypes browserType) {
            switch (browserType) {
                case BrowserTypes.All:
                    return new[] {
                                     DesiredCapabilities.Firefox(),
                                     DesiredCapabilities.InternetExplorer(),
                                     DesiredCapabilities.Chrome()
                                 };
                case BrowserTypes.FireFox:
                    return new[] { DesiredCapabilities.Firefox() };
                case BrowserTypes.IE:
                    return new[] { DesiredCapabilities.InternetExplorer() };
                case BrowserTypes.Chrome:
                    return new[] { DesiredCapabilities.Chrome() };
                default:
                    throw new ArgumentOutOfRangeException("browserType");
            }
        }

        private IEnumerable<WebTester> GetTester(BrowserTypes browserType) {
            switch (browserType) {
                case BrowserTypes.All:
                    return new WebTester[] {
                                     new IETest(),
                                     new FireFoxTest(),
                                     new ChromeTest(),
                                 };
                case BrowserTypes.FireFox:
                    return new[] { new FireFoxTest() };
                case BrowserTypes.IE:
                    return new[] { new IETest() };
                case BrowserTypes.Chrome:
                    return new[] { new ChromeTest() };
                default:
                    throw new ArgumentOutOfRangeException("browserType");
            }
        }

        protected void RequiresSite(string siteURL) {
            Debug.WriteLine("Checking for availability of " + siteURL);

            int attempt = 0;
            bool started = false;
            while (!started && attempt < 5) {
                attempt++;
                try {
                    WebRequest request = WebRequest.Create(siteURL);
                    {
                        request.Timeout = 10000;
                        using (request.GetResponse()) {
                            started = true;
                            Logger.InfoFormat("Site {0} is running", siteURL);
                            Debug.WriteLine("Site is running");
                        }
                    }
                } catch (WebException ex) {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                        AssertFail(string.Format("Failed to start site : {0} {1}", siteURL, ex));

                    Debug.WriteLine(string.Format("Timed out waiting for site {0} {1}", siteURL, ex));
                    Logger.InfoFormat("Site {0} is running", string.Format("{0} {1}", siteURL, ex));
                }
            }
            if (!started) {
                AssertFail(string.Format("Failed to start site : {0}", siteURL));
            }
        }

        protected abstract void AssertFail(string s);

        protected void SetBaseURL(string baseURL) {
            this.baseURL = baseURL;
        }

        protected void BeforeTest(Action beforeTestAction) {
            Logger.Info("Before Test ::");
            beforeTestAction();
        }

        protected void TestData(Action createAction, Action deleteAction) {
            Logger.Info("TestData ::");
            deleteCommand = deleteAction;
            deleteCommand();
            createAction();
        }
        protected void AfterTest() {
            Logger.Info("After Test ::");
            deleteCommand();
        }
    }
}