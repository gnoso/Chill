using Cassini;

namespace Chill.Testing
{
    // basically provides a singleton wrapper around cassini servers to make sure that the same server
    // doesn't try to run more than once
    public class AppHarness
    {
        private static AppHarness _harness;

        public static void RunApplication(string path, int port)
        {
            if (_harness != null)
            {
                // if we're running a different app right now, stop it and clear it out
                if (!_harness.Matches(path, port))
                {
                    _harness.Stop();
                    _harness = null;
                }
            }

            if (_harness == null)
            {
                _harness = new AppHarness(path, port);
            }

            _harness.Start();
        }

        public static void StopApplication()
        {
            if (_harness != null)
            {
                _harness.Stop();
            }
        }

        private string _path;
        private int _port;
        private Server _server;

        public AppHarness(string path, int port)
        {
            _path = path;
            _port = port;
        }

        public bool Matches(string path, int port)
        {
            return _path == path && _port == port;
        }

        public void Start()
        {
            _server = new Server(_port, "/", _path);
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}
