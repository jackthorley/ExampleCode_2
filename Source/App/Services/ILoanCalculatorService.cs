namespace App.Services
{
    using Models;

    public interface ILoanCalculatorService
    {
        Loan Calculate(AvailableMoneyData availableMoneyData, int loanAmount);
    }
}