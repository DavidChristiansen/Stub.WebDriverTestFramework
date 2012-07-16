using OpenQA.Selenium.IE;

namespace net.davedoes.acceptancetestframework.Local {
    public class IETest : WebTester {
        public IETest() {
            _driver = new InternetExplorerDriver();
        }
    }
}