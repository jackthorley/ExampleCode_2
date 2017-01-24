namespace App.Validators
{
    public interface IValidator<in T>
    {
        bool Validate(T value);
    }
}