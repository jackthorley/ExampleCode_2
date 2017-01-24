namespace App.Tests.Validators
{
    using App.Validators;
    using NUnit.Framework;

    [TestFixture(1000, true)]
    [TestFixture(15000, true)]
    [TestFixture(15100, false)]
    [TestFixture(1012, false)]
    [TestFixture(900, false)]
    public class LoanAmountValidatorTests
    {
        private readonly int _loanAmount;
        private readonly bool _isValid;
        private bool _result;

        public LoanAmountValidatorTests(int loanAmount, bool isValid)
        {
            _loanAmount = loanAmount;
            _isValid = isValid;
        }

        [TestFixtureSetUp]
        public void Given()
        {
            var validator = new LoanAmountValidator();
            _result = validator.Validate(_loanAmount);
        }

        [Test]
        public void ThenTheExpectedResultIsVerified()
        {
            Assert.That(_result, Is.EqualTo(_isValid));
        }
    }
}
