namespace App.Tests
{
    using App.Data;
    using App.Services;
    using Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ProcessorTestsWithValidParameters
    {
        private Mock<ILoanCalculatorService> _loanCalculator;
        private Mock<IDataLoader<AvailableMoneyData>> _loanDataLoader;

        private string _fileLocationArguement;
        private string _loanAmountArguement;
        private AvailableMoneyData _availableMoneyData;
        private Loan _expectedLoan;
        private ModelResult<Loan> _result;
        private Mock<IConverter<ProgramArguments, ModelResult<LoanRequest>>> _converter;
        private LoanRequest _loanRequest;

        [TestFixtureSetUp]
        public void Given()
        {
            _loanAmountArguement = "12345";
            _fileLocationArguement = "testfilelocation";

            _availableMoneyData = new AvailableMoneyData(null);
            _loanRequest = new LoanRequest("asdasdasd", 1234);
            _expectedLoan = new Loan();
            
            _converter = new Mock<IConverter<ProgramArguments, ModelResult<LoanRequest>>>();
            _converter.Setup(c => c.Convert(It.IsAny<ProgramArguments>()))
                      .Returns(new ModelResult<LoanRequest>(ResultStatus.Successful, _loanRequest));

            _loanDataLoader = new Mock<IDataLoader<AvailableMoneyData>>();
            _loanDataLoader
                .Setup(dl => dl.Load(It.IsAny<string>()))
                .Returns(_availableMoneyData);

            _loanCalculator = new Mock<ILoanCalculatorService>();
            _loanCalculator
                .Setup(lc => lc.Calculate(It.IsAny<AvailableMoneyData>(), It.IsAny<int>()))
                .Returns(_expectedLoan);

            var processor = new LoanProcessor(_converter.Object, _loanDataLoader.Object, _loanCalculator.Object);
            _result = processor.Process(_fileLocationArguement,_loanAmountArguement);
        }

        [Test]
        public void ThenTheModelValidatorIsCalled()
        {
            _converter.Verify(c => c.Convert(It.Is<ProgramArguments>(pa => VerifyArguements(pa))));
        }
        
        private bool VerifyArguements(ProgramArguments pa)
        {
            return pa.FileLocation == _fileLocationArguement && pa.LoanAmount == _loanAmountArguement;
        }

        [Test]
        public void ThenTheLoanFileIsLoaded()
        {
            _loanDataLoader.Verify(dl => dl.Load(_loanRequest.FileLocation));
        }

        [Test]
        public void ThenTheLoanCalculatorIsCalled()
        {
            _loanCalculator.Verify(lc => lc.Calculate(_availableMoneyData, _loanRequest.LoanAmount));
        }

        [Test]
        public void ThenTheCorrectLoanIsReturned()
        {
            Assert.That(_result.Value, Is.EqualTo(_expectedLoan));
            Assert.That(_result.Status, Is.EqualTo(ResultStatus.Successful));
        }
    }

    [TestFixture]
    public class ProcessorTestsWithInvalidParameters
    {
        private string _fileLocation;
        private string _loanAmount;
        private ModelResult<Loan> _result;
        private Mock<IConverter<ProgramArguments, ModelResult<LoanRequest>>> _converter;

        [TestFixtureSetUp]
        public void Given()
        {
            _loanAmount = "12345";
            _fileLocation = "testfilelocation";

            _converter = new Mock<IConverter<ProgramArguments, ModelResult<LoanRequest>>>();
            _converter.Setup(c => c.Convert(It.IsAny<ProgramArguments>()))
                      .Returns(new ModelResult<LoanRequest>(ResultStatus.InvalidArgument));

            var processor = new LoanProcessor(_converter.Object, null, null);
            _result = processor.Process(_fileLocation, _loanAmount);
        }

        [Test]
        public void ThenTheModelValidatorIsCalled()
        {
            _converter
                .Verify(c => c.Convert(It.Is<ProgramArguments>(pa => VerifyArguements(pa))));
        }

        private bool VerifyArguements(ProgramArguments pa)
        {
            return pa.FileLocation == _fileLocation && pa.LoanAmount == _loanAmount;
        }

        [Test]
        public void ThenTheResultIsAFailure()
        {
            Assert.That(_result.Status, Is.EqualTo(ResultStatus.InvalidArgument));
        }
    }
}
