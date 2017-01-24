namespace App
{
    using System;
    using Models;

    class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                GenerateInvalidArguementAlert();
                Environment.Exit(0);
            }
           
            var loanProcessor = ServiceFactory.CreateLoanProcessor();
            var modelResult = loanProcessor.Process(args[0], args[1]);

            if (modelResult.Status != ResultStatus.Successful)
            {
                GenerateInvalidArguementAlert();
                Environment.Exit(0);
            }
        }

        private static void GenerateInvalidArguementAlert()
        {
            Console.WriteLine("Usage: cmd> App_Loan_Test.exe loanfile.csv loanamount");
        }
    }
}