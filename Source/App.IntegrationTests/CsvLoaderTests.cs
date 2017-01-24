namespace App.IntegrationTests
{
    using Data.CsvHelper;
    using NUnit.Framework;

    [TestFixture]
    public class CsvLoaderTests
    {
        private ICsvHelper _csvHelper;

        [SetUp]
        public void Given()
        {
            //Should dynamically create file.
            var csvHelperWrapper = new CsvHelperWrapper();
            _csvHelper = csvHelperWrapper.Create("test.csv");
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _csvHelper.Dispose();
        }

        [Test]
        public void ThenTheValuesReturnedAreValid()
        {
            var row = _csvHelper.Read();
            Assert.That(row.Length, Is.EqualTo(3));
            Assert.That(row[0], Is.EqualTo("test"));
            Assert.That(row[1], Is.EqualTo("test1"));
            Assert.That(row[2], Is.EqualTo("test2"));
        }
    }
}
