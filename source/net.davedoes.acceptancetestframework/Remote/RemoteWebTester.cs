using System;
using OpenQA.Selenium;

namespace net.davedoes.acceptancetestframework.Remote {
    public class RemoteWebTester : WebTester {
        public RemoteWebTester(IWebDriver driver) {
            _driver = driver;
        }

        public override void Run(Action<IWebDriver> action) {
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(4));
            try {
                action(_driver);
            }
            catch {
                TakeScreenshot(_driver, string.Format("c:\\temp\\UATPics\\{0}_{1}.png", this.GetType(), DateTime.Now.ToString("ddMMyyyyhhmmss")));
            }
        }
    }
}