using OpenQA.Selenium.IE;

namespace net.davedoes.acceptancetestframework.Local {
    public class IETest : WebTester {
        public IETest()
        {
           var options =  new InternetExplorerOptions();
            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            _driver = new InternetExplorerDriver(options);
        }
    }
}