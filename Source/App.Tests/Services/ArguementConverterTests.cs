namespace App.Tests.Services
{
    using App.Services;
    using App.Validators;
    using Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class LoanModelConverterWithValidInputsTests
    {
        private const int LOAN_AMOUNT = 1000;
        private ModelResult<LoanRequest> _result;
        private string _fileLocation;
        private Mock<IValidator<string>> _fileValidator;
        private Mock<IValidator<int>> _loanAmountValidator;

        [SetUp]
        public void Given()
        {
            _fileLocation = "test.csv";

            _fileValidator = new Mock<IValidator<string>>();
            _fileValidator
                .Setup(fv => fv.Validate(It.IsAny<string>()))
                .Returns(true);

            _loanAmountValidator = new Mock<IValidator<int>>();
            _loanAmountValidator
                .Setup(fv => fv.Validate(It.IsAny<int>()))
                .Returns(true);

            var loanModelConverter = new LoanModelConverter(_fileValidator.Object, _loanAmountValidator.Object);
            _result = loanModelConverter.Convert(new ProgramArguments(_fileLocation, LOAN_AMOUNT.ToString()));
        }

        [Test]
        public void ThenTheFileValidatorIsCalled()
        {
            _fileValidator.Verify(v => v.Validate(_fileLocation));
        }

        [Test]
        public void ThenTheModelResultIsSuccessful()
        {
            Assert.That(_result.Status, Is.EqualTo(ResultStatus.Successful));
            Assert.That(_result.Value.FileLocation, Is.EqualTo(_fileLocation));
            Assert.That(_result.Value.LoanAmount, Is.EqualTo(LOAN_AMOUNT));
        }
    }

    [TestFixture]
    public class LoanModelConverterWithInvalidCsvInputsTests
    {
        private const int LOAN_AMOUNT = 1000;
        private ModelResult<LoanRequest> _result;
        private string _fileLocation;
        private Mock<IValidator<string>> _fileValidator;

        [SetUp]
        public void Given()
        {
            _fileLocation = "test";

            _fileValidator = new Mock<IValidator<string>>();
            _fileValidator
                .Setup(fv => fv.Validate(It.IsAny<string>()))
                .Returns(false);

            var loanModelConverter = new LoanModelConverter(_fileValidator.Object, null);
            _result = loanModelConverter.Convert(new ProgramArguments(_fileLocation, LOAN_AMOUNT.ToString()));
        }

        [Test]
        public void ThenTheFileValidatorIsCalled()
        {
            _fileValidator.Verify(v => v.Validate(_fileLocation));
        }

        [Test]
        public void ThenTheModelResultIsUnsuccessful()
        {
            Assert.That(_result.Status, Is.EqualTo(ResultStatus.InvalidArgument));
            Assert.That(_result.Value, Is.Null);
        }
    }

    [TestFixture]
    public class LoanModelConverterWithNonDigitLoanAmountInputsTests
    {
        private ModelResult<LoanRequest> _result;
        private string _fileLocation;
        private Mock<IValidator<string>> _fileValidator;

        [SetUp]
        public void Given()
        {
            _fileLocation = "test.csv";
            _fileValidator = new Mock<IValidator<string>>();
            _fileValidator
                .Setup(fv => fv.Validate(It.IsAny<string>()))
                .Returns(true);

            var loanModelConverter = new LoanModelConverter(_fileValidator.Object, null);
            _result = loanModelConverter.Convert(new ProgramArguments(_fileLocation, "asdasd"));
        }

        [Test]
        public void ThenTheFileValidatorIsCalled()
        {
            _fileValidator.Verify(v => v.Validate(_fileLocation));
        }

        [Test]
        public void ThenTheModelResultIsUnsuccessful()
        {
            Assert.That(_result.Status, Is.EqualTo(ResultStatus.InvalidArgument));
            Assert.That(_result.Value, Is.Null);
        }
    }

    [TestFixture]
    public class LoanModelConverterWithInvalidLoanAmountInputsTests
    {
        private ModelResult<LoanRequest> _result;
        private string _fileLocation;
        private Mock<IValidator<string>> _fileValidator;
        private Mock<IValidator<int>> _loanAmountValidator;

        [SetUp]
        public void Given()
        {
            _fileLocation = "test.csv";
            _fileValidator = new Mock<IValidator<string>>();
            _fileValidator
                .Setup(fv => fv.Validate(It.IsAny<string>()))
                .Returns(true);

            _loanAmountValidator = new Mock<IValidator<int>>();
            _loanAmountValidator
                .Setup(fv => fv.Validate(It.IsAny<int>()))
                .Returns(false);

            var loanModelConverter = new LoanModelConverter(_fileValidator.Object, _loanAmountValidator.Object);
            _result = loanModelConverter.Convert(new ProgramArguments(_fileLocation, "asdasd"));
        }

        [Test]
        public void ThenTheFileValidatorIsCalled()
        {
            _fileValidator.Verify(v => v.Validate(_fileLocation));
        }

        [Test]
        public void ThenTheModelResultIsUnsuccessful()
        {
            Assert.That(_result.Status, Is.EqualTo(ResultStatus.InvalidArgument));
            Assert.That(_result.Value, Is.Null);
        }
    }
}
