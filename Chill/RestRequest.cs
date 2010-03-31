using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Chill
{
    // Class that wraps up HttpWebRequest to make it a bit easier to swallow.
    public class RestRequest
    {
        private string _url;
        private string _body;

        public RestRequest(string url)
        {
            _url = url;
        }

        public HttpWebResponse Request(string method, string body, string contentType)
        {
            var request = (HttpWebRequest) WebRequest.Create("http://localhost:3456/TestPage.aspx");
            request.Method = method;

            if (method == "POST")
            {
                request.ContentType = contentType;
                StreamWriter bodyStream = new StreamWriter(request.GetRequestStream());
                bodyStream.Write(body);
                bodyStream.Close();
            }

            return (HttpWebResponse) request.GetResponse();
        }

        // does a GET request
        public RestResponse Get()
        {
            return new RestResponse(Request("GET", "", ""));
        }

        // does a POST request without any body
        public RestResponse Post()
        {
            return Post(null);
        }

        // does a POST request, using the parameters given
        public RestResponse Post(object postParams)
        {
            var queryString = new StringBuilder();
            foreach(var property in GetProperties(postParams))
            {
                queryString.Append(HttpUtility.UrlEncode(property.Name));
                queryString.Append("=");
                queryString.Append(HttpUtility.UrlEncode(property.Value.ToString()));
                queryString.Append("&");
            }
            queryString.Remove(queryString.Length - 2, 1);

            return Post(queryString.ToString());
        }

        // Makes a post request, 
        public RestResponse Post(string body)
        {
            return new RestResponse(Request("POST", body, "application/x-www-form-urlencoded"));
        }

        // I think I saw this in some of Phil Haack's writing. How's that for attribution? - Alan
        private static IEnumerable<PropertyValue> GetProperties(object o)
        {
            if (o != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(o);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(o);
                    if (val != null)
                    {
                        yield return new PropertyValue { Name = prop.Name, Value = val };
                    }
                }
            }
        }
        private sealed class PropertyValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }
}
