namespace App.Services
{
    public interface IConverter<in T, out T1>
    {
        T1 Convert(T input);
    }
}