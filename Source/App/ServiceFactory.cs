namespace App
{
    using Data;
    using Data.CsvHelper;
    using Services;
    using Validators;

    static internal class ServiceFactory
    {
        public static LoanProcessor CreateLoanProcessor()
        {
            return new LoanProcessor(CreateLoanModelConverter(), CreateDataLoader(), CreateLoanCalculator());
        }

        private static LoanModelConverter CreateLoanModelConverter()
        {
            return new LoanModelConverter(new FileValidator(), new LoanAmountValidator());
        }

        public static LoanCalculatorService CreateLoanCalculator()
        {
            return new LoanCalculatorService();
        }

        public static CsvDataLoader CreateDataLoader()
        {
            return new CsvDataLoader(new CsvHelperWrapper());
        }
    }
}