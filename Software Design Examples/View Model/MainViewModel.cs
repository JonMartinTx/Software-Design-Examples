using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using Software_Design_Examples.Annotations;
using Software_Design_Examples.Models.Beverages;
using Software_Design_Examples.Properties;
using Software_Design_Examples.View_Model.Read_Data_From_File;
using Software_Design_Examples.View_Model.UsefulExtensions;

namespace Software_Design_Examples.View_Model
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties

        #region Private Properties

        private double _paymentAmount;
        private double _availableChange;
        private double _changeDue;
        private int _cokeAmount;
        private int _dietCokeAmount;
        private int _waterAmount;
        private int _lemonadeAmount;
        private double _cashInMachine;

        #endregion

        #region Products

        public string Coke => $"{CokeName}: {CokeAmount}";
        public string DietCoke => $"{DietCokeName}: {DietCokeAmount}";
        public string Water => $"{WaterName}: {WaterAmount}";
        public string Lemonade => $"{LemonadeName}: {LemonadeAmount}";

        public string CokeName { get; } = "Coke";

        public int CokeAmount
        {
            get => _cokeAmount;
            set
            {
                if (value == _cokeAmount) return;
                _cokeAmount = value;
                OnPropertyChanged(nameof(Coke));
            }
        }

        public string DietCokeName { get; } = "Diet Coke";

        public int DietCokeAmount
        {
            get => _dietCokeAmount;
            set
            {
                if (value == _dietCokeAmount) return;
                _dietCokeAmount = value;
                OnPropertyChanged(nameof(DietCoke));
            }
        }

        public string WaterName { get; } = "Water";

        public int WaterAmount
        {
            get => _waterAmount;
            set
            {
                if (value == _waterAmount) return;
                _waterAmount = value;
                OnPropertyChanged(nameof(Water));
            }
        }

        public string LemonadeName { get; } = "Lemonade";

        public int LemonadeAmount
        {
            get => _lemonadeAmount;
            set
            {
                if (value == _lemonadeAmount) return;
                _lemonadeAmount = value;
                OnPropertyChanged(nameof(Lemonade));
            }
        }

        public double CashInMachine
        {
            get => _cashInMachine;
            set
            {
                if (value.Equals(_cashInMachine)) return;
                _cashInMachine = value;
                OnPropertyChanged(nameof(CashInMachine));
            }
        }

        public double CokePrice { get; set; }
        public double DietCokePrice { get; set; }
        public double WaterPrice { get; set; }
        public double LemonadePrice { get; set; }

        private bool _inManagerMode;

        #endregion

        #region Payment

        public double PaymentAmount
        {
            get => _paymentAmount;
            set
            {
                if (value.Equals(_paymentAmount)) return;
                _paymentAmount = value;
                OnPropertyChanged(nameof(PaymentAmount));
            }
        }

        public double AvailableChange
        {
            get => _availableChange;
            set
            {
                if (value.Equals(_availableChange)) return;
                _availableChange = value;
                OnPropertyChanged(nameof(AvailableChange));
            }
        }

        public double ChangeDue
        {
            get => _changeDue;
            set
            {
                if (value.Equals(_changeDue)) return;
                _changeDue = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Visibility

        public bool InManagerMode
        {
            get => _inManagerMode;
            set
            {
                _inManagerMode = value;
                OnPropertyChanged(nameof(InManagerMode));
                OnPropertyChanged(nameof(InCustomerMode));
            }
        }

        public bool InCustomerMode => !InManagerMode;

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        public VendingList VendingList { get; set; }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            var instance = InventoryAndLedgerSingleton.Instance;
           
            var text = File.ReadAllText(@"C:\Users\Jon Martin\source\repos\Software Design Examples\Software Design Examples\Resources\Config.txt");
            var _ = new ReadFile(text);

            VendingList = new VendingList();
            VendingList.VendingItems.Add(new VendingItem
            {
                Name = instance.BeverageInventory.CokeName,
                Price = instance.BeverageInventory.CokePrice.ToPriceString(),
                ButtonImage = new System.Windows.Controls.Image{Source = UsefulExtensions.UsefulExtensions.ToImage(Resources.Coke_Round_Logo)},
                ImageSource = UsefulExtensions.UsefulExtensions.GetNameOf(() => Resources.Coke_Round_Logo)
            });

            VendingList.VendingItems.Add(new VendingItem
            {
                Name = instance.BeverageInventory.DietCokeName,
                Price = instance.BeverageInventory.DietCokePrice.ToPriceString(),
                ButtonImage = new System.Windows.Controls.Image { Source = UsefulExtensions.UsefulExtensions.ToImage(Resources.Diet_Coke_Round_Logo) },
                ImageSource = UsefulExtensions.UsefulExtensions.GetNameOf(() => Resources.Diet_Coke_Round_Logo)
            });

            VendingList.VendingItems.Add(new VendingItem
            {
                Name = instance.BeverageInventory.WaterName,
                Price = instance.BeverageInventory.WaterPrice.ToPriceString(),
                ButtonImage = new System.Windows.Controls.Image { Source = UsefulExtensions.UsefulExtensions.ToImage(Resources.Water_Round_Logo) },
                ImageSource = UsefulExtensions.UsefulExtensions.GetNameOf(() => Resources.Water_Round_Logo)
            });

            VendingList.VendingItems.Add(new VendingItem
            {
                Name = instance.BeverageInventory.LemonadeName,
                Price = instance.BeverageInventory.LemonadePrice.ToPriceString(),
                ButtonImage = new System.Windows.Controls.Image { Source = UsefulExtensions.UsefulExtensions.ToImage(Resources.Lemonade_Round_Logo) },
                ImageSource = UsefulExtensions.UsefulExtensions.GetNameOf(() => Resources.Lemonade_Round_Logo)
            });

            CokePrice = instance.BeverageInventory.CokePrice;
            DietCokePrice = instance.BeverageInventory.DietCokePrice;
            WaterPrice = instance.BeverageInventory.WaterPrice;
            LemonadePrice = instance.BeverageInventory.LemonadePrice;

            CokeAmount = instance.BeverageInventory.NumberOfCokesInStock;
            DietCokeAmount = instance.BeverageInventory.NumberOfDietCokesInStock;
            WaterAmount = instance.BeverageInventory.NumberOfWatersInStock;
            LemonadeAmount = instance.BeverageInventory.NumberOfLemonadesInStock;

            AvailableChange = 0;
            PaymentAmount = 0;
        }

        #endregion

        #region Methods

        public Action PurchaseBeverage(Beverages purchase)
        {
            return purchase switch
            {
                Models.Beverages.Coke => () => BuyCoke(),
                Diet_Coke => BuyDietCoke(),
                Models.Beverages.Water => BuyWater(),
                Models.Beverages.Lemonade => BuyLemonade()
            };
        }

        private Action BuyCoke()
        {
            return () =>
            {
                if (!(PaymentAmount >= CokePrice) || CokeAmount <= 0) return;
                PaymentAmount -= CokePrice;
                CalculateChangeDue(CokePrice);
                CashInMachine += CokePrice;
                CokeAmount--;
                InventoryAndLedgerSingleton.Instance.BeverageInventory.NumberOfCokesInStock--;
            };
        }

        private Action BuyDietCoke()
        {
            return () =>
            {
                if (!(PaymentAmount >= DietCokePrice) || DietCokeAmount <= 0) return;
                PaymentAmount -= DietCokePrice;
                CalculateChangeDue(DietCokePrice);
                CashInMachine += DietCokePrice;
                DietCokeAmount--;
                InventoryAndLedgerSingleton.Instance.BeverageInventory.NumberOfDietCokesInStock--;
            };
        }

        private Action BuyWater()
        {
            return () =>
            {
                if (!(PaymentAmount >= WaterPrice) || WaterAmount <= 0) return;
                PaymentAmount -= WaterPrice;
                CalculateChangeDue(WaterPrice);
                CashInMachine += WaterPrice;
                WaterAmount--;
                InventoryAndLedgerSingleton.Instance.BeverageInventory.NumberOfWatersInStock--;
            };
        }

        private Action BuyLemonade()
        {
            return () =>
            {
                if (!(PaymentAmount >= LemonadePrice) || LemonadeAmount <= 0) return;
                PaymentAmount -= LemonadePrice;
                CalculateChangeDue(LemonadePrice);
                CashInMachine += LemonadePrice;
                LemonadeAmount--;
                InventoryAndLedgerSingleton.Instance.BeverageInventory.NumberOfLemonadesInStock--;
            };
        }

        private void CalculateChangeDue(double price)
        {
            ChangeDue = PaymentAmount - price;
        }

        public void AddPayment(double amount)
        {
            PaymentAmount += amount;
        }

        

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


    }
}
