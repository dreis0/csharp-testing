using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class IntallerHelperTests
    {
        InstallerHelper _installer;
        Mock<IDownloadClient> _downloadClient;

        [SetUp]
        public void SetUp()
        {
            _downloadClient = new Mock<IDownloadClient>();
            _installer = new InstallerHelper(_downloadClient.Object);
        }

        [Test]
        public void DowloadInstaller_WhenCalled_ReturnsTrue()
        {
            bool result = _installer.DownloadInstaller("teste", "teste");
            Assert.That(result, Is.True);
        }

        [Test]
        public void DowloadInstaller_WhenErrorOccours_ReturnsFalse()
        {
            _downloadClient
                .Setup(dc => dc.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            bool result = _installer.DownloadInstaller("teste", "teste");
            Assert.That(result, Is.False);
        }

    }
}
