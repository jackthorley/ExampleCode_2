namespace App.Tests.Validators
{
    using App.Validators;
    using NUnit.Framework;

    [TestFixture]
    public class FileValidatorWithValidCsvTests
    {
        private bool _isValid;

        [SetUp]
        public void Given()
        {
            var validator = new FileValidator();
            _isValid = validator.Validate("test.csv");
        }

        [Test]
        public void ThenTheValidationReturnsSuccess()
        {
            Assert.That(_isValid, Is.True);
        }
    }

    [TestFixture]
    public class FileValidatorWithInvalidCsvTests
    {
        private bool _isValid;

        [SetUp]
        public void Given()
        {
            var validator = new FileValidator();
            _isValid = validator.Validate("test");
        }

        [Test]
        public void ThenTheValidationReturnsSuccess()
        {
            Assert.That(_isValid, Is.False);
        }
    }
}
