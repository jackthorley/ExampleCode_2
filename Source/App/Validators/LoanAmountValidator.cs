namespace App.Validators
{
    internal class LoanAmountValidator : IValidator<int>
    {
        public bool Validate(int value)
        {
            if (value < 1000 || value > 15000)
                return false;

            if (value%100 != 0)
                return false;

            return true;
        }
    }
}