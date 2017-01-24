namespace App.Models
{
    public class LoanRequest
    {
        public string FileLocation { get; }
        public int LoanAmount { get; }

        public LoanRequest(string fileLocation, int loanAmount)
        {
            FileLocation = fileLocation;
            LoanAmount = loanAmount;
        }
    }
}