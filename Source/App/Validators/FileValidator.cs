namespace App.Validators
{
    internal class FileValidator : IValidator<string>
    {
        public bool Validate(string value)
        {
            return value.Contains(".csv");
        }
    }
}