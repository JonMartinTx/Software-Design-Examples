using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Software_Design_Examples.Models.Configuration_File;
using Software_Design_Examples.View_Model.Read_Data_From_File;

namespace Software_Design_Examples.View_Model.Write_Data_to_File
{
    public class WriteDataToFile
    {
        public string FilePath { get; private set; }
        public string FileContents { get; private set; }
        public ConfigFile? ConfigFile { get; private set; }
        private InventoryAndLedgerSingleton instance;

        internal WriteDataToFile(string filePath)
        {
            FilePath = filePath;
            instance = InventoryAndLedgerSingleton.Instance;
            ConfigFile = instance.ConfiguartionFile;
            LoadInventoryToFileData();
            FileContents = JsonConvert.SerializeObject(ConfigFile, Formatting.Indented);
            WriteToFile();
        }

        private void WriteToFile()
        {
            var test =
                @"C:\Users\Jon Martin\source\repos\Software Design Examples\Software Design Examples\Resources\Config.txt";
            File.WriteAllText(test, FileContents);
        }

        #region Load Data from Singleton


        public Action SelectProductInventoryToLoad(Inventory inventory) => inventory.ProductName.ToLower() switch
        {
            "coke" => () => LoadCokeInfo(inventory),
            "diet coke" => () => LoadDietCokeInfo(inventory),
            "water" => () => LoadWaterInfo(inventory),
            "lemonade" => () => LoadLemonadeInfo(inventory),
            _ => throw new ArgumentOutOfRangeException("Invalid Beverage Type in Configuration File.")
        };

        private void LoadInventoryToFileData()
        {
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
            inventory.NumberAvailable = instance.BeverageInventory.NumberOfCokesInStock;
            inventory.RefillAmount = instance.BeverageInventory.CokeRefillRequestAmount;
            inventory.MaxAvailable = instance.BeverageInventory.MaxNumberOfCokesAvailable;
            inventory.Price = instance.BeverageInventory.CokePrice;
        }

        private void LoadDietCokeInfo(Inventory? inventory)
        {
            if (NullCheckObject(inventory)) return;
            inventory.NumberAvailable = instance.BeverageInventory.NumberOfDietCokesInStock;
            inventory.RefillAmount = instance.BeverageInventory.DietCokeRefillRequestAmount;
            inventory.MaxAvailable = instance.BeverageInventory.MaxNumberOfDietCokesAvailable;
            inventory.Price = instance.BeverageInventory.DietCokePrice;
        }

        private void LoadWaterInfo(Inventory? inventory)
        {
            if (NullCheckObject(inventory)) return;
            inventory.NumberAvailable = instance.BeverageInventory.NumberOfWatersInStock;
            inventory.RefillAmount = instance.BeverageInventory.WaterRefillRequestAmount;
            inventory.MaxAvailable = instance.BeverageInventory.MaxNumberOfWatersAvailable;
            inventory.Price = instance.BeverageInventory.WaterPrice;
        }

        private void LoadLemonadeInfo(Inventory? inventory)
        {
            if (NullCheckObject(inventory)) return;
            inventory.NumberAvailable = instance.BeverageInventory.NumberOfDietCokesInStock;
            inventory.RefillAmount = instance.BeverageInventory.DietCokeRefillRequestAmount;
            inventory.MaxAvailable = instance.BeverageInventory.MaxNumberOfDietCokesAvailable;
            inventory.Price = instance.BeverageInventory.LemonadePrice;
        }

#endregion

        #region Load Cash Ledger into Singleton

        private void LoadCashLedger()
        {
            if (NullCheckObject(ConfigFile)) return;
            ConfigFile!.Ledger.CashOnHand = instance.CashLedger.CashInMachine;
            ConfigFile.Ledger.MaxAmount = instance.CashLedger.MaxCashInMachine;
            ConfigFile.Ledger.RefillAmount = instance.CashLedger.MinCashInMachine;
            ConfigFile.Ledger.NumberOfNickels = instance.CashLedger.NumberOfNickels;
            ConfigFile.Ledger.NumberOfDimes = instance.CashLedger.NumberOfDimes;
            ConfigFile.Ledger.NumberOfQuarters = instance.CashLedger.NumberOfQuarters;
            ConfigFile.Ledger.NumberOfOnes = instance.CashLedger.NumberOfOnes;
            ConfigFile.Ledger.NumberOfFives = instance.CashLedger.NumberOfFives;
            ConfigFile.Ledger.NumberOfTens = instance.CashLedger.NumberOfTens;
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

