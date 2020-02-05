using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using TestNinja.Mocking;
using TestNinja.UnitTests.Mocking.Mocks;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        VideoService _videoService;
        Mock<IFileReader> _fileReader;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_fileReader.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void ReadVideoTitle_FileHasContent_ReturnsTitle()
        {
            _fileReader
                .Setup(fr => fr.Read("video.txt"))
                .Returns(JsonConvert.SerializeObject(new Video { Title = "Teste" }));

            var result = _videoService.ReadVideoTitle();
            Assert.That(result, Is.EqualTo("Teste"));
        }
    }
}
