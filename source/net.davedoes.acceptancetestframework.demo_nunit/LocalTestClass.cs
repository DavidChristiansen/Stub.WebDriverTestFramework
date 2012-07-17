using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using net.davedoes.acceptancetestframework.demo_nunit.Infrastructure.Local;

namespace net.davedoes.acceptancetestframework.demo_nunit {
    [TestFixture]
    public class LocalTestClass : LocalTestBase {
        [Test]
        public void Lumia900() {
            BeforeTest(InitTest);
            TestScript(driver => {
                driver.Navigate().GoToUrl(baseURL + "/en-us/default.aspx");
                Assert.AreEqual("Microsoft Corporation: Software, Smartphones, Online, Games, Cloud Computing, IT Business Technology, Downloads", driver.Title);
                driver.FindElement(By.Id("ctl00_ctl14_ItemsRepeater_ctl00_ItemLink")).Click();
                driver.FindElement(By.XPath("//a[contains(text(),'Windows Phone')]")).Click();
                Assert.AreEqual("Windows Phone | Cell Phones, Mobile Downloads, Mobile Apps, and More | Windows Phone 7", driver.Title);
                driver.FindElement(By.LinkText("Buy")).Click();
                Assert.AreEqual("Smartphone | Compare Windows 7 Phones | Windows Mobile Phones | Windows Phone 7", driver.Title);
                driver.FindElement(By.XPath("//img[@alt='Nokia Lumia 900 (Black)']")).Click();
            }, BrowserTypes.IE);
        }
    }
}