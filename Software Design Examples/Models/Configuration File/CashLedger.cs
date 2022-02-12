using System;

namespace Software_Design_Examples.Models.Configuration_File
{
    [Serializable]
    public class CashLedger
    {
        public double Income { get; set; }
        public double CashOnHand { get; set; }
        public double RefillAmount { get; set; }
        public double MaxAmount { get; set; }
        public int NumberOfQuarters { get; set; }
        public int NumberOfDimes { get; set; }
        public int NumberOfNickels { get; set; }
        public int NumberOfOnes { get; set; }
        public int NumberOfFives { get; set; }
        public int NumberOfTens { get; set; }
    }
}
