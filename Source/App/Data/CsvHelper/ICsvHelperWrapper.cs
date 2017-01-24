namespace App.Data.CsvHelper
{
    public interface ICsvHelperWrapper
    {
        ICsvHelper Create(string fileLocation);
    }
}