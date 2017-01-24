namespace App.Services
{
    using Models;
    using Validators;

    internal class LoanModelConverter : IConverter<ProgramArguments, ModelResult<LoanRequest>>
    {
        private readonly IValidator<string> _fileValidator;
        private readonly IValidator<int> _loanAmountValidator;

        public LoanModelConverter(IValidator<string> fileValidator, IValidator<int> loanAmountValidator)
        {
            _fileValidator = fileValidator;
            _loanAmountValidator = loanAmountValidator;
        }

        public ModelResult<LoanRequest> Convert(ProgramArguments input)
        {
            if(!_fileValidator.Validate(input.FileLocation))
                return new ModelResult<LoanRequest>(ResultStatus.InvalidArgument);

            int loanAmount;
            if(!int.TryParse(input.LoanAmount, out loanAmount))
                return new ModelResult<LoanRequest>(ResultStatus.InvalidArgument);

            if (!_loanAmountValidator.Validate(loanAmount))
                return new ModelResult<LoanRequest>(ResultStatus.InvalidArgument);
            
            return new ModelResult<LoanRequest>(ResultStatus.Successful, new LoanRequest(input.FileLocation,  loanAmount));
        }
    }
}