using System;
using System.Net;
using Chill.Testing;
using NUnit.Framework;

namespace Chill.Unit
{
    [TestFixture]
    [WithService(@".\Chill.TestApp\", 3456)]
    public class AppHarnessAndChillTestBaseTest : ChillTestBase
    {
        [Test]
        public void TestAppWorks()
        {
            var request = (HttpWebRequest) WebRequest.Create("http://localhost:3456/TestPage.aspx");
            var response = (HttpWebResponse) request.GetResponse();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
