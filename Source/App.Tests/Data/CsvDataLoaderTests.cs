namespace App.Tests.Data
{
    using System.Collections.Generic;
    using System.Globalization;
    using App.Data;
    using App.Data.CsvHelper;
    using Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class CsvDataLoaderTests
    {
        private Mock<ICsvHelperWrapper> _csvWrapper;
        private Mock<ICsvHelper> _csvHelper;
        private string _fileLocation;
        private AvailableMoneyData _result;
        private decimal _rate;
        private int _money;

        [SetUp]
        public void Given()
        {
            _fileLocation = "fileLocation";

            _rate = 1m;
            _money = 3;

            var rows = new List<string[]>
            {
                new[] {"header", "header", "header"},
                new[] {"yolo", _rate.ToString(CultureInfo.InvariantCulture), _money.ToString()},
                null
            };

            _csvHelper = new Mock<ICsvHelper>();
            _csvHelper
                .SetupSequence(h => h.Read())
                .Returns(rows[0])
                .Returns(rows[1])
                .Returns(rows[2]);

            _csvWrapper = new Mock<ICsvHelperWrapper>();
            _csvWrapper
                .Setup(w => w.Create(It.IsAny<string>()))
                .Returns(_csvHelper.Object);

            var csvDataLoader = new CsvDataLoader(_csvWrapper.Object);
            _result = csvDataLoader.Load(_fileLocation);
        }

        [Test]
        public void ThenTheWrapperIsCalledToCreateAHelper()
        {
            _csvWrapper.Verify(w => w.Create(_fileLocation));
        }

        [Test]
        public void ThenTheHelperReturnsTheRows()
        {
            _csvHelper.Verify(h => h.Read(), Times.Exactly(3));
        }

        [Test]
        public void ThenTheDataIsReturned()
        {
            Assert.That(_result.AvailableMoney[0].Money, Is.EqualTo(_money));
            Assert.That(_result.AvailableMoney[0].Rate, Is.EqualTo(_rate));
        }

        [Test]
        public void ThenTheCsvHelperIsDisposed()
        {
            _csvHelper.Verify(h => h.Dispose());
        }
    }
}