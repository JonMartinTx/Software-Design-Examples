namespace Software_Design_Examples.Models.Inventory_Management
{
    public class CashInventory
    {
        public double CashInMachine { get; set; }
        public double MaxCashInMachine { get; set; }
        public double MinCashInMachine { get; set; }

        #region Cash on Hand for Change

        public int NumberOfQuarters { get; set; }
        public int NumberOfDimes { get; set; }
        public int NumberOfNickels { get; set; }
        public int NumberOfOnes { get; set; }
        public int NumberOfFives { get; set; }
        public int NumberOfTens { get; set; }

        #endregion

    }
}
