namespace net.davedoes.acceptancetestframework.demo_nunit.Infrastructure.Local {
    public abstract class LocalTestBase : LocalNUnitTestBase {
        protected override void InitTest() {
            SetBaseURL("http://www.microsoft.com/");
            RequiresSite("http://www.microsoft.com");
            base.InitTest();
        }
    }
}