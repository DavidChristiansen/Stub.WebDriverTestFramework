namespace net.davedoes.acceptancetestframework.demo_nunit.Infrastructure.Remote {
    public abstract class RemoteTestBase : RemoteNUnitTestBase {
        protected override void InitTest() {
            SetBaseURL("http://www.microsoft.com/");
            RequiresSite("http://www.microsoft.com");
            base.InitTest();
        }
    }
}