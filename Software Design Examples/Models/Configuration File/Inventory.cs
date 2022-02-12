using System;

namespace Software_Design_Examples.Models.Configuration_File
{
    [Serializable]
    public class Inventory
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int MaxAvailable { get; set; }
        public int RefillAmount { get; set; }
        public int NumberAvailable { get; set; }
        public double Price { get; set; }

    }
}
