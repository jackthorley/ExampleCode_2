namespace App.Data
{
    using System.Collections.Generic;
    using CsvHelper;
    using Models;

    internal class CsvDataLoader : IDataLoader<AvailableMoneyData>
    {
        private readonly ICsvHelperWrapper _csvHelperWrapper;

        public CsvDataLoader(ICsvHelperWrapper csvHelperWrapper)
        {
            _csvHelperWrapper = csvHelperWrapper;
        }

        public AvailableMoneyData Load(string fileLocation)
        {
            var availableMonies = new List<AvailableMoney>();
            using (var csvHelper = _csvHelperWrapper.Create(fileLocation))
            {
                csvHelper.Read();

                while (true)
                {
                    var csvValues = csvHelper.Read();
                    if (csvValues == null)
                        break;

                    var rate = decimal.Parse(csvValues[1]);
                    var availableMoney = int.Parse(csvValues[2]);

                    availableMonies.Add(new AvailableMoney
                    {
                        Rate = rate,
                        Money = availableMoney
                    });
                }
            }

            return new AvailableMoneyData(availableMonies);
        }
    }
}