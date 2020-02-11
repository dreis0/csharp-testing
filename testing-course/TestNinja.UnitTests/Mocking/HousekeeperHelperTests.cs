using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HousekeeperHelperTests
    {
        Mock<IHouseKeeperRepository> _repository;
        Mock<IHouseKeeperFileManager> _fileManager;
        Mock<IEmailFileManager> _emailManager;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IHouseKeeperRepository>();
            _fileManager = new Mock<IHouseKeeperFileManager>();
            _emailManager = new Mock<IEmailFileManager>();
        }

        [Test]
        public void SendStatementEmails_NoHousekeepers_ReturnsTrue()
        {
            _repository
                .Setup(rp => rp.GetHousekeepers())
                .Returns(new List<Housekeeper>().AsQueryable());

            var result = HousekeeperHelper.SendStatementEmails(DateTime.Now, _repository.Object, _fileManager.Object, _emailManager.Object);

            _fileManager.Verify(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
            _emailManager.Verify(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            Assert.IsTrue(result);
        }

        [Test]
        public void SendStatementEmails_EmptyEmail__ReturnsTrue()
        {
            _repository
                .Setup(rp => rp.GetHousekeepers())
                .Returns(new List<Housekeeper>
                {
                    new Housekeeper
                    {
                        Oid = 1,
                        Email = string.Empty
                    }
                }.AsQueryable());

            var result = HousekeeperHelper.SendStatementEmails(DateTime.Now, _repository.Object, _fileManager.Object, _emailManager.Object);

            _fileManager.Verify(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
            _emailManager.Verify(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            Assert.IsTrue(result);
        }

        [Test]
        public void SendStatementEmails_EmptyStatementFileName_ReturnsTrue()
        {
            _repository
                .Setup(rp => rp.GetHousekeepers())
                .Returns(new List<Housekeeper>
                {
                    new Housekeeper
                    {
                        Oid = 1,
                        Email = "email@email.com"
                    }
                }.AsQueryable());

            _fileManager
                .Setup(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(string.Empty);

            var result = HousekeeperHelper.SendStatementEmails(DateTime.Now, _repository.Object, _fileManager.Object, _emailManager.Object);

            _fileManager.Verify(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _emailManager.Verify(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            Assert.IsTrue(result);
        }

        [Test]
        public void SendStatementEmails_SendsEmail_ReturnsTrue()
        {
            _repository
                .Setup(rp => rp.GetHousekeepers())
                .Returns(new List<Housekeeper>
                {
                    new Housekeeper
                    {
                        Oid = 1,
                        Email = "email@email.com"
                    }
                }.AsQueryable());

            _fileManager
                .Setup(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns("filename");

            var result = HousekeeperHelper.SendStatementEmails(DateTime.Now, _repository.Object, _fileManager.Object, _emailManager.Object);

            _fileManager.Verify(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _emailManager.Verify(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            Assert.IsTrue(result);
        }

        [Test]
        public void SendStatementEmails_FailsToSendEmail_ThrowsEsception()
        {
            _repository
                .Setup(rp => rp.GetHousekeepers())
                .Returns(new List<Housekeeper>
                {
                    new Housekeeper
                    {
                        Oid = 1,
                        Email = "email@email.com"
                    }
                }.AsQueryable());

            _fileManager
                .Setup(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns("filename");

            _emailManager
                .Setup(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            var result = HousekeeperHelper.SendStatementEmails(DateTime.Now, _repository.Object, _fileManager.Object, _emailManager.Object);

            _fileManager.Verify(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _emailManager.Verify(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            Assert.IsTrue(result);
        }
    }
}
