namespace Software_Design_Examples.Models.Inventory_Management
{
    public class BeverageInventory
    {
        #region Properties

        #region Available Products

        public bool CokeIsAvailable => NumberOfCokesInStock > 0;
        public bool DietCokeIsAvailable => NumberOfDietCokesInStock > 0;
        public bool WaterIsAvailable => NumberOfWatersInStock > 0;
        public bool LemonadeIsAvailable => NumberOfLemonadesInStock > 0;

        #endregion

        #region Name

        public string CokeName { get; } = "Coke";
        public string DietCokeName { get; } = "Diet Coke";
        public string WaterName { get; } = "Water";
        public string LemonadeName { get; } = "Lemonade";

        #endregion

        #region Prices

        public double CokePrice { get; set; }
        public double DietCokePrice { get; set; }
        public double WaterPrice { get; set; }
        public double LemonadePrice { get; set;}

        #endregion

        #region RefillRequest Amounts

        public int CokeRefillRequestAmount { get; set; }
        public int DietCokeRefillRequestAmount { get; set; }
        public int WaterRefillRequestAmount { get; set; }
        public int LemonadeRefillRequestAmount { get; set; }

        #endregion

        #region In Need of Refill

        public bool CokeNeedsRefill => NumberOfCokesInStock <= CokeRefillRequestAmount;
        public bool DietCokeNeedsRefill => NumberOfDietCokesInStock <= DietCokeRefillRequestAmount;
        public bool WaterNeedsRefill => NumberOfWatersInStock <= WaterRefillRequestAmount;
        public bool LemonadeNeedsRefill => NumberOfLemonadesInStock <= LemonadeRefillRequestAmount;

        #endregion

        #region Maximum Available

        public int MaxNumberOfCokesAvailable { get; set; }
        public int MaxNumberOfDietCokesAvailable { get; set; }
        public int MaxNumberOfWatersAvailable { get; set; }
        public int MaxNumberOfLemonadesAvailable { get; set; }

        #endregion

        #region In Stock

        public int NumberOfCokesInStock { get; set; } = 0;
        public int NumberOfDietCokesInStock { get; set; } = 0;
        public int NumberOfWatersInStock { get; set; } = 0;
        public int NumberOfLemonadesInStock { get; set; } = 0;

        #endregion

        #endregion

    }
}
