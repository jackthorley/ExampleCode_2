namespace App.Data
{
    public interface IDataLoader<out T>
    {
        T Load(string fileLocation);
    }
}