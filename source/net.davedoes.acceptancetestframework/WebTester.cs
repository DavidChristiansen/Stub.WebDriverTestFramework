using System;
using System.Drawing.Imaging;
using Common.Logging;
using OpenQA.Selenium;

namespace net.davedoes.acceptancetestframework {
    public abstract class WebTester {
        protected static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        protected IWebDriver _driver;

        public void ShutDown() {
            try {
                _driver.Close();
            } catch (Exception) {
                // Ignore errors if unable to close the browser
            }
        }

        public virtual void Run(Action<IWebDriver> action) {
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(4));

            try {
                action(_driver);
            } catch (Exception ex) {
                Logger.Error(ex);
                TakeScreenshot(_driver, string.Format("c:\\temp\\UATPics\\{0}_{1}.png", this.GetType(), DateTime.Now.ToString("ddMMyyyyhhmmss")));
                throw;
            }
        }

        public void TakeScreenshot(IWebDriver driver, string saveLocation) {
            try {
                ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
                if (screenshotDriver != null) {
                    Screenshot screenshot = screenshotDriver.GetScreenshot();
                    screenshot.SaveAsFile(saveLocation, ImageFormat.Png);
                }
            } catch {

            }
        }
    }
}