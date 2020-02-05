using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        IDownloadClient _client;
        private string _setupDestinationFile;

        public InstallerHelper(IDownloadClient client = null)
        {
            _client = client ?? new DownloadClient();
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _client.DownloadFile(
                    string.Format("http://example.com/{0}/{1}", customerName, installerName),
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }


    }
}