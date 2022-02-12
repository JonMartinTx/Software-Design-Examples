using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Software_Design_Examples.Annotations;
using Software_Design_Examples.Models.Configuration_File;
using Software_Design_Examples.Models.Inventory_Management;

namespace Software_Design_Examples.View_Model.Read_Data_From_File
{
    public sealed class InventoryAndLedgerSingleton : INotifyPropertyChanged
    {
        #region This stuff lets me use this singleton to hold the data from the file and show it on the view of this application.

        #region Singleton Instance Stuff -- Don't worry about this.

        private InventoryAndLedgerSingleton()
        {
            BeverageInventory = new BeverageInventory();
            CashLedger = new CashInventory();
            ServiceRequests = new List<ActiveServiceRequests>();
        }
        private static readonly Lazy<InventoryAndLedgerSingleton> Lazy = new(() => new InventoryAndLedgerSingleton());
        public static InventoryAndLedgerSingleton Instance => Lazy.Value;

        #endregion

        #region Event Handler Stuff -- Don't Worry about this.

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #endregion

        #region Private Properties

        private CashInventory _cashInventory;
        private BeverageInventory _beverageInventory;
        private List<ActiveServiceRequests> _serviceRequests;

        #endregion

        #region Accessors / Mutators

        public CashInventory CashLedger
        {
            get => _cashInventory;
            set
            {
                _cashInventory = value;
                OnPropertyChanged(nameof(CashLedger)); //Event Handler stuff, don't worry about this.
            }
        }

        public BeverageInventory BeverageInventory
        {
            get => _beverageInventory;
            set
            {
                _beverageInventory = value;
                OnPropertyChanged(nameof(BeverageInventory)); //Event Handler stuff, don't worry about this.
            }
        }

        public List<ActiveServiceRequests> ServiceRequests
        {
            get => _serviceRequests;
            set
            {
                _serviceRequests = value;
                OnPropertyChanged(nameof(ServiceRequests)); //Event Handler stuff, don't worry about this.
            }
        }

        public ConfigFile ConfiguartionFile { get; set; }

        #endregion

        #region Lambda Fields

        public bool CokeInventoryIsNotAtMaximum =>
            BeverageInventory.MaxNumberOfCokesAvailable > BeverageInventory.NumberOfCokesInStock;
        public bool DietCokeInventoryIsNotAtMaximum =>
            BeverageInventory.MaxNumberOfDietCokesAvailable > BeverageInventory.NumberOfDietCokesInStock;
        public bool WaterInventoryIsNotAtMaximum =>
            BeverageInventory.MaxNumberOfWatersAvailable > BeverageInventory.NumberOfWatersInStock;
        public bool LemonadeInventoryIsNotAtMaximum =>
            BeverageInventory.MaxNumberOfLemonadesAvailable > BeverageInventory.NumberOfLemonadesInStock;

        #endregion

        #region Methods

        #region Property Changed Event Invocator -- Don't Worry About This

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #endregion
    }
}
