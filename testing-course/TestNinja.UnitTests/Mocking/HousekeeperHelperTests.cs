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
        Mock<IMessager> _messager;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IHouseKeeperRepository>();
            _fileManager = new Mock<IHouseKeeperFileManager>();
            _emailManager = new Mock<IEmailFileManager>();
            _messager = new Mock<IMessager>();
        }

        [Test]
        public void SendStatementEmails_NoHousekeepers_NoActionsPerformed()
        {
            _repository
                .Setup(rp => rp.GetHousekeepers())
                .Returns(new List<Housekeeper>().AsQueryable());

            var result = HousekeeperService.SendStatementEmails(DateTime.Now, _repository.Object, _fileManager.Object, _emailManager.Object, _messager.Object);

            _fileManager.Verify(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
            _emailManager.Verify(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _messager.Verify(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>()), Times.Never);

            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void SendStatementEmails_EmptyEmail_NoActionsPerformed(string email)
        {
            MockRepository(email);

            var result = HousekeeperService.SendStatementEmails(DateTime.Now, _repository.Object, _fileManager.Object, _emailManager.Object, _messager.Object);

            _fileManager.Verify(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
            _emailManager.Verify(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _messager.Verify(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>()), Times.Never);

            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void SendStatementEmails_EmptyStatementFileName_DoesNotSendEmail(string filename)
        {
            MockRepository("email@email.com");
            MockFileManager(filename);

            var result = HousekeeperService.SendStatementEmails(DateTime.Now, _repository.Object, _fileManager.Object, _emailManager.Object, _messager.Object);

            _fileManager.Verify(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _emailManager.Verify(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _messager.Verify(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>()), Times.Never);

            Assert.That(result, Is.True);
        }

        [Test]
        public void SendStatementEmails_SendsEmail_ReturnsTrue()
        {
            MockRepository("email@email.com");
            MockFileManager("filename");

            var result = HousekeeperService.SendStatementEmails(DateTime.Now, _repository.Object, _fileManager.Object, _emailManager.Object, _messager.Object);

            _fileManager.Verify(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _emailManager.Verify(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _messager.Verify(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>()), Times.Never);

            Assert.That(result, Is.True);
        }

        [Test]
        public void SendStatementEmails_FailsToSendEmail_ThrowsException()
        {
            MockRepository("email@email.com");
            MockFileManager("filename");

            _emailManager
                .Setup(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            var result = HousekeeperService.SendStatementEmails(DateTime.Now, _repository.Object, _fileManager.Object, _emailManager.Object, _messager.Object);

            _fileManager.Verify(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
            _emailManager.Verify(em => em.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _messager.Verify(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>()), Times.Once);

            Assert.That(result, Is.False);
        }

        public void MockRepository(string mockedEmail)
        {
            _repository
                .Setup(rp => rp.GetHousekeepers())
                .Returns(new List<Housekeeper>
                {
                    new Housekeeper
                    {
                        Oid = 1,
                        Email = mockedEmail
                    }
                }.AsQueryable());
        }

        public void MockFileManager(string mockedFilename)
        {
            _fileManager
                .Setup(fm => fm.SaveStatement(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(mockedFilename);
        }
    }
}
