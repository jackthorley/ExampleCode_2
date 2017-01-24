namespace App.Data.CsvHelper
{
    using System;

    public interface ICsvHelper : IDisposable
    {
        string[] Read();
    }
}