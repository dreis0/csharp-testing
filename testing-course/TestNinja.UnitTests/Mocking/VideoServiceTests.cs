using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;
using TestNinja.UnitTests.Mocking.Mocks;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        VideoService _videoService;
        Mock<IFileReader> _fileReader;
        Mock<IVideoRepository> _videoRepository;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
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

        [Test]
        public void GetUnprocessedVideosAsCsv_NoUnprocessedVideos_ReturnsEmpty()
        {
            _videoRepository
                .Setup(vr => vr.GetUnprocessedVideos())
                .Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetUnprocessedVideoAsCsv_SomeUnprocessedVideos_ReturnsCsv()
        {
            _videoRepository
                .Setup(vr => vr.GetUnprocessedVideos())
                .Returns(new List<Video>
                {
                    new Video { Id = 1 },
                    new Video { Id = 2 },
                    new Video { Id = 3 },
                });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
