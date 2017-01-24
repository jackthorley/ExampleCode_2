namespace App.Data.CsvHelper
{
    using System.IO;
    using global::CsvHelper;

    public class CsvHelper : ICsvHelper
    {
        private readonly FileStream _fileStream;
        private readonly StreamReader _streamReader;
        private readonly ICsvParser _createParser;

        public CsvHelper(string fileLocation)
        {
            _fileStream = new FileStream(fileLocation, FileMode.OpenOrCreate);
            _streamReader = new StreamReader(_fileStream);
            _createParser = new CsvFactory().CreateParser(_streamReader);
        }

        public void Dispose()
        {
            _fileStream.Dispose();
            _streamReader.Dispose();
        }

        public string[] Read()
        {
            return _createParser.Read();
        }
    }
}