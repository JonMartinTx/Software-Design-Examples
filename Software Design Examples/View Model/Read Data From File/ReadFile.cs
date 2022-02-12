using System;
using System.IO;
using Newtonsoft.Json;
using Software_Design_Examples.Models.Configuration_File;

namespace Software_Design_Examples.View_Model.Read_Data_From_File
{
    internal class ReadFile
    {
        public string FilePath { get; private set; }
        public string FileContents { get; private set; }
        public ConfigFile? ConfigFile { get; private set; }
        private InventoryAndLedgerSingleton instance;

        internal ReadFile(string fileContents)
        {
            instance = InventoryAndLedgerSingleton.Instance;
            FileContents = fileContents;
            ConfigFile = JsonConvert.DeserializeObject<ConfigFile>(fileContents);
            instance.ConfiguartionFile = JsonConvert.DeserializeObject<ConfigFile>(fileContents);
            LoadValues();
        }

        private void LoadValues()
        {
            if (string.IsNullOrEmpty(FileContents)) return;
            ConfigFile = JsonConvert.DeserializeObject<ConfigFile>(FileContents) ?? null;
            LoadInventoryFromFileData();
            LoadCashLedger();
            LoadActiveServiceRequests();
        }

        #region Load Inventory Values into Singleton

        public Action SelectProductInventoryToLoad(Inventory inventory) => inventory.ProductName.ToLower() switch
        {
            "coke" => () => LoadCokeInfo(inventory),
            "diet coke" => () => LoadDietCokeInfo(inventory),
            "water" => () => LoadWaterInfo(inventory),
            "lemonade" => () => LoadLemonadeInfo(inventory),
            _ => throw new ArgumentOutOfRangeException("Invalid Beverage Type in Configuration File.")
        };

        private void LoadInventoryFromFileData()
        {
            if (NullCheckObject(ConfigFile)) return;
            var inventories = ConfigFile.Inventories;
            inventories.ForEach(delegate (Inventory inventory)
            {
                SelectProductInventoryToLoad(inventory).Invoke();
            });
        }

        private void LoadCokeInfo(Inventory? inventory)
        {
            instance ??= InventoryAndLedgerSingleton.Instance;
            if (NullCheckObject(inventory)) return;
            instance.BeverageInventory.NumberOfCokesInStock = inventory.NumberAvailable;
            instance.BeverageInventory.CokeRefillRequestAmount = inventory.RefillAmount;
            instance.BeverageInventory.MaxNumberOfCokesAvailable = inventory.MaxAvailable;
            instance.BeverageInventory.CokePrice = inventory.Price;
        }

        private void LoadDietCokeInfo(Inventory? inventory)
        {
            if (NullCheckObject(inventory)) return;
            instance.BeverageInventory.NumberOfDietCokesInStock = inventory.NumberAvailable;
            instance.BeverageInventory.DietCokeRefillRequestAmount = inventory.RefillAmount;
            instance.BeverageInventory.MaxNumberOfDietCokesAvailable = inventory.MaxAvailable;
            instance.BeverageInventory.DietCokePrice = inventory.Price;
        }

        private void LoadWaterInfo(Inventory? inventory)
        {
            if (NullCheckObject(inventory)) return;
            instance.BeverageInventory.NumberOfWatersInStock = inventory.NumberAvailable;
            instance.BeverageInventory.WaterRefillRequestAmount = inventory.RefillAmount;
            instance.BeverageInventory.MaxNumberOfWatersAvailable = inventory.MaxAvailable;
            instance.BeverageInventory.WaterPrice = inventory.Price;
        }

        private void LoadLemonadeInfo(Inventory? inventory)
        {
            if (NullCheckObject(inventory)) return;
            instance.BeverageInventory.NumberOfLemonadesInStock = inventory.NumberAvailable;
            instance.BeverageInventory.LemonadeRefillRequestAmount = inventory.RefillAmount;
            instance.BeverageInventory.MaxNumberOfLemonadesAvailable = inventory.MaxAvailable;
            instance.BeverageInventory.LemonadePrice = inventory.Price;
        }

        #endregion

        #region Load Cash Ledger into Singleton

        private void LoadCashLedger()
        {
            if (NullCheckObject(ConfigFile)) return;
            instance.CashLedger.CashInMachine = ConfigFile!.Ledger.CashOnHand;
            instance.CashLedger.MaxCashInMachine = ConfigFile.Ledger.MaxAmount;
            instance.CashLedger.MinCashInMachine = ConfigFile.Ledger.RefillAmount;
            instance.CashLedger.NumberOfNickels = ConfigFile.Ledger.NumberOfNickels;
            instance.CashLedger.NumberOfDimes = ConfigFile.Ledger.NumberOfDimes;
            instance.CashLedger.NumberOfQuarters = ConfigFile.Ledger.NumberOfQuarters;
            instance.CashLedger.NumberOfOnes = ConfigFile.Ledger.NumberOfOnes;
            instance.CashLedger.NumberOfFives = ConfigFile.Ledger.NumberOfFives;
            instance.CashLedger.NumberOfTens = ConfigFile.Ledger.NumberOfTens;
        }

        #endregion

        #region Load Active Service Requests

        private void LoadActiveServiceRequests()
        {
            if (NullCheckObject(ConfigFile)) return;
            var serviceRequests = ConfigFile!.ActiveRequests;

            foreach (var request in serviceRequests)
            {
                instance.ServiceRequests.Add(new ActiveServiceRequests
                {
                    Id = request.Id,
                    DateOfRequest = request.DateOfRequest,
                    ServiceRequestType = request.ServiceRequestType,
                    Message = request.Message
                });
            }
        }

        #endregion

        private bool NullCheckObject(object? obj)
        {
            return obj == null;
        }
    }
}
