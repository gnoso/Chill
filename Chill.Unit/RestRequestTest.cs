using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Chill.Testing;
using NUnit.Framework;

namespace Chill.Unit
{
    [TestFixture]
    [WithService(@"./Chill.TestApp/", 3456)]
    public class RestRequestTest : ChillTestBase
    {
        [Test]
        public void TestRequest()
        {
            var request = new RestRequest(BaseUrl + "TestPage.aspx");
            var response = request.Request("POST", "test=" + HttpUtility.UrlEncode("Bring the awesome."),
                "application/x-www-form-urlencoded");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.StatusDescription);

            var responseBody = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Assert.IsTrue(responseBody.Contains("Bring the awesome."), responseBody);
        }

        [Test]
        public void TestGet()
        {
            var response = new RestRequest(BaseUrl + "TestPage.aspx").Get();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestPost()
        {
            var response = new RestRequest(BaseUrl + "TestPage.aspx").Post();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);           
        }

        [Test]
        public void TestPostWithParams()
        {
            var response = new RestRequest(BaseUrl + "TestPage.aspx").Post(new {test = "So rad."});
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
