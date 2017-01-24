namespace App
{
    using Data;
    using Models;
    using Services;

    public class LoanProcessor
    {
        private readonly IConverter<ProgramArguments, ModelResult<LoanRequest>> _modelConverter;
        private readonly IDataLoader<AvailableMoneyData> _dataLoader;
        private readonly ILoanCalculatorService _loanCalculatorService;

        public LoanProcessor(IConverter<ProgramArguments, ModelResult<LoanRequest>> modelConverter, IDataLoader<AvailableMoneyData> dataLoader, ILoanCalculatorService loanCalculatorService)
        {
            _modelConverter = modelConverter;
            _dataLoader = dataLoader;
            _loanCalculatorService = loanCalculatorService;
        }

        public ModelResult<Loan> Process(string fileLocation, string loanAmount)
        {
            var convertedModelResult = _modelConverter.Convert(new ProgramArguments(fileLocation, loanAmount));

            if (convertedModelResult.Status != ResultStatus.Successful)
                return new ModelResult<Loan>(ResultStatus.InvalidArgument);

            var availableMoneyData = _dataLoader.Load(convertedModelResult.Value.FileLocation);
            var loan = _loanCalculatorService.Calculate(availableMoneyData, convertedModelResult.Value.LoanAmount);

            return new ModelResult<Loan>(ResultStatus.Successful, loan);
        }
    }
}