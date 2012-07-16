using OpenQA.Selenium.Firefox;

namespace net.davedoes.acceptancetestframework.Local {
    public class FireFoxTest : WebTester {
        public FireFoxTest() {
            _driver = new FirefoxDriver();
        }
    }
}