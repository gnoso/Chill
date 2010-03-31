using System;
using System.Reflection;
using NUnit.Framework;

namespace Chill.Testing
{
    public abstract class ChillTestBase
    {
        private WithServiceAttribute _withService;

        // Useful helper for getting to the url that the running app harness is at.
        protected string BaseUrl
        {
            get { return string.Format("http://localhost:{0}/", Port); }
        }
        protected int Port
        {
            get { return _withService.Port; }
        }

        [TestFixtureSetUp]
        public void ChillTestFixtureSetUp()
        {
            // find the with service attribute
            MemberInfo info = this.GetType();
            var attributes = (WithServiceAttribute[])info.GetCustomAttributes(typeof (WithServiceAttribute), true);
            if (attributes.Length < 1)
            {
                throw new InvalidOperationException("ServiceTestBase derived classes must have a WithService attribute.");
            }
            _withService = attributes[0];

            AppHarness.RunApplication(_withService.Path, _withService.Port);

            TestFixtureSetUp();
        }

        // override this method to provide TestFixtureSetUp methods in derived test classes
        public virtual void TestFixtureSetUp()
        {
        }

        [TestFixtureTearDown]
        public void ChillTestFixtureTearDown()
        {
            TestFixtureTearDown();
            AppHarness.StopApplication();
        }

        // override this method to provide TestFixtureTearDown methods in derived test classes
        public virtual void TestFixtureTearDown()
        {
        }
    }
}
