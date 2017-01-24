namespace App.Models
{
    public class ProgramArguments
    {
        public string FileLocation { get; }
        public string LoanAmount { get; }

        public ProgramArguments(string fileLocation, string loanAmount)
        {
            FileLocation = fileLocation;
            LoanAmount = loanAmount;
        }
    }
}