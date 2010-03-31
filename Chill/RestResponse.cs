using System.Net;

namespace Chill
{
    public class RestResponse
    {
        private HttpWebResponse _response;
        public RestResponse(HttpWebResponse response)
        {
            _response = response;
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return _response.StatusCode;
            }
        }
    }
}
