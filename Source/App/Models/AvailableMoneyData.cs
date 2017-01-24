namespace App.Models
{
    using System.Collections.Generic;

    public class AvailableMoneyData
    {
        public List<AvailableMoney> AvailableMoney { get; }

        public AvailableMoneyData(List<AvailableMoney> availableMonies)
        {
            AvailableMoney = availableMonies;
        }
    }

    public class AvailableMoney
    {
        public decimal Rate { get; set; }
        public int Money { get; set; }
    }
}