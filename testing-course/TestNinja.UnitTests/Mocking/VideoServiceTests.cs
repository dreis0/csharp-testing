using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Mocking;
using TestNinja.UnitTests.Mocking.Mocks;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _videoService = new VideoService(new FakeFileReader());
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            var result = _videoService.ReadVideoTitle();
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}
