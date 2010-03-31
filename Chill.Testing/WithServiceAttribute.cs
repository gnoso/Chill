using System;

namespace Chill.Testing
{
    // Use this attribute to highlight the path and port for testing a service with ServiceTestBase
    [AttributeUsage(AttributeTargets.Class)]
    public class WithServiceAttribute : System.Attribute
    {
        private string _path;
        public string Path
        {
            get
            {
                return System.IO.Path.GetFullPath(_path);
            } 
            set
            {
                _path = value;
            }
        }
        public int Port { get; set; }

        public WithServiceAttribute(string path, int port)
        {
            Path = path;
            Port = port;
        }
    }
}
