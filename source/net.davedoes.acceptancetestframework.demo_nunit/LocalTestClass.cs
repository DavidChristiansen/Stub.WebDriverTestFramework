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
                Assert.AreEqual("Downloads and support", driver.FindElement(By.XPath("//div[@id='ctl00_ctl17_BodyRepeater_ctl00_ctl01_ColumnRepeater_ctl00_RowRepeater_ctl02_CellRepeater_ctl00_ctl01_clientID']/div/h3")).Text);
                driver.FindElement(By.CssSelector("a.hpImage_Link > img.hpImage_Img")).Click();
                Assert.AreEqual("4G LTE lightning-fast speed", driver.FindElement(By.XPath("//div[@id='ctl00_wpcBBDeviceList1_listWrapper']/div/div[2]/div[2]/div[2]/div")).Text);
                Assert.AreEqual("From $99.99", driver.FindElement(By.LinkText("From $99.99")).Text);
                driver.FindElement(By.XPath("//img[@alt='Nokia Lumia 900 (Black)']")).Click();
                Assert.AreEqual("Nokia Lumia 900 (Black)", driver.FindElement(By.CssSelector("h2.name.styleD")).Text);
                Assert.AreEqual("Nokia Lumia 900", driver.Title);
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.LinkText("exact:Buy for $99.99*")).Text, "^exact:Buy for \\$99\\.99[\\s\\S]*$"));
            }, BrowserTypes.All);
        }
    }
}