using NUnit.Framework;

namespace net.davedoes.acceptancetestframework.demo_nunit.Infrastructure.Remote {
    public abstract class RemoteNUnitTestBase : RemoteWebTestBase {
        protected override void AssertTrue(bool boolToAssert, string message) {
            Assert.IsTrue(boolToAssert, message);
        }

        protected override void AssertFalse(bool boolToAssert, string message) {
            Assert.IsFalse(boolToAssert, message);
        }

        protected override void AssertFail(string s) {
            Assert.Fail(s);
        }
    }
}
