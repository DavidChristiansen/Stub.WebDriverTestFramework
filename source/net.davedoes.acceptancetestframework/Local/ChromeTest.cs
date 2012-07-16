using OpenQA.Selenium.Chrome;

namespace net.davedoes.acceptancetestframework.Local {
    public class ChromeTest : WebTester {
        public ChromeTest() {
            _driver = new ChromeDriver();
        }
    }
}