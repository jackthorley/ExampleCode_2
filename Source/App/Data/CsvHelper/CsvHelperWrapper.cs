namespace App.Data.CsvHelper
{
    public class CsvHelperWrapper : ICsvHelperWrapper
    {
        public ICsvHelper Create(string fileLocation)
        {
            return new CsvHelper(fileLocation);
        }
    }
}
