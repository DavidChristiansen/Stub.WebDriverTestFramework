using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using net.davedoes.acceptancetestframework.Remote;

namespace net.davedoes.acceptancetestframework {
    public abstract class RemoteWebTestBase : WebTestBase {
        private string _remoteTestServer;
        private const string _remoteTestServerFormat = "http://{0}:4444/wd/hub";
        public abstract string RemoteTestServerName { get; }
        protected override void InitTest()
        {
            base.InitTest();
            SetRemoteServer(RemoteTestServerName);
            RequiresSite(_remoteTestServer);
        }

        private void SetRemoteServer(string remoteTestServerName) {
            _remoteTestServer = string.Format(_remoteTestServerFormat, remoteTestServerName);
        }

        protected override void TestScript(Action<IWebDriver> action, params BrowserTypes[] browserTypes) {
            foreach (IEnumerable<DesiredCapabilities> testEngine in browserTypes.Select(GetRemoteTester)) {
                testEngine.All(capabilities => {
                    RemoteWebTester tester = null;
                    try {
                        var remoteAddress = new Uri(_remoteTestServer);
                        IWebDriver driver = new RemoteWebDriver(remoteAddress, capabilities);
                        tester = new RemoteWebTester(driver);
                        tester.Run(action);
                    }
                    finally {
                        if (tester != null) {
                            tester.ShutDown();
                        }
                    }
                    return true;
                });
            }
        }
    }
}