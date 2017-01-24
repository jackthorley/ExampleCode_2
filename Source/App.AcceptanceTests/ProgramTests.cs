namespace App.AcceptanceTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class GivenAStandardLoanAmount
    {
        private string _requestedAmount;

        [TestFixtureSetUp]
        public void WhenTheServiceIsRunning()
        {
            _requestedAmount = "test.csv 1000";
            Application.Start(_requestedAmount);
        }

        [Test]
        public void ThenResultIsCorrect()
        {
            var expectedOutput = CreateCalculatorOutput(_requestedAmount, "7","30.78","1108.10");

            Assert.That(Application.Output, Is.EqualTo(expectedOutput));
        }

        private static string CreateCalculatorOutput(string requestAmount, string rate, string monthRepayment, string totalRepayment)
        {
            var nl = Environment.NewLine;
            return $"Requested amount: £{requestAmount}{nl} Rate: {rate}%{nl} Monthly repayment: £{monthRepayment}{nl} Total repayment: £{totalRepayment}";
        }
    }
    
    [TestFixture]
    public class GivenInvalidArgumentOrder
    {
        
        [TestFixtureSetUp]
        public void WhenTheServiceIsRunning()
        {
            Application.Start("100302 test.csv");
        }

        [Test]
        public void ThenResultIsCorrect()
        {
            Assert.That(Application.Output, Is.EqualTo("Usage: cmd> App_Loan_Test.exe loanfile.csv loanamount\r\n"));
        }
    }

    [TestFixture("")]
    [TestFixture("asdad asdasd asdasdas")]
    public class GivenInvalidArgumentLength
    {
        private readonly string _arguments;

        public GivenInvalidArgumentLength(string arguments)
        {
            _arguments = arguments;
        }

        [TestFixtureSetUp]
        public void WhenTheServiceIsRunning()
        {
            Application.Start(_arguments);
        }

        [Test]
        public void ThenResultIsCorrect()
        {
            Assert.That(Application.Output, Is.EqualTo("Usage: cmd> App_Loan_Test.exe loanfile.csv loanamount\r\n"));
        }
    }
}
