using System.Net;

namespace TestNinja.Mocking
{
    public interface IDownloadClient
    {
        void DownloadFile(string address, string fileName);
    }

    public class DownloadClient : IDownloadClient
    {
        public void DownloadFile(string address, string fileName)
        {
            var client = new WebClient();

            client.DownloadFile(address, fileName);
        }
    }

}
