using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using Software_Design_Examples.Annotations;
using Software_Design_Examples.Properties;

namespace Software_Design_Examples.View_Model
{
    public class PurchaseViewModel : INotifyPropertyChanged
    {
        private BitmapImage _backGroundImage;
        private string _beverageSelected;
        private double _paymentAmount;
        private double _changeDue;
        private double _price;

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

        public double ChangeDue
        {
            get => _changeDue;
            set
            {
                if (value.Equals(_changeDue)) return;
                _changeDue = value;
                OnPropertyChanged(nameof(ChangeDue));
            }
        }

        public string BeverageSelected
        {
            get => _beverageSelected;
            set
            {
                if (value.Equals(_beverageSelected)) return;
                _beverageSelected = value;
                OnPropertyChanged(nameof(BeverageSelected));
            }
        }

        public BitmapImage BackGroundImage
        {
            get => _backGroundImage;
            set
            {
                if (value.Equals(_backGroundImage)) return;
                _backGroundImage = value;
                OnPropertyChanged(nameof(BackGroundImage));
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                if (value.Equals(_price)) return;
                _price = value;
                OnPropertyChanged();
            }
        }

        public string ResponseText => BeverageSelected switch
        {
            "Coke" => "Would you like to purchase a Coke?" + Environment.NewLine +
                      $"You have inserted {ConvertToCurrencyString(PaymentAmount)}." + Environment.NewLine +
                      $"Cokes cost {ConvertToCurrencyString(Price)}." + Environment.NewLine +
                      $"Your Change Due is {ConvertToCurrencyString(ChangeDue)}.",
            "Diet Coke" => "Would you like to purchase a Diet Coke?" + Environment.NewLine +
                           $"You have inserted {ConvertToCurrencyString(PaymentAmount)}." + Environment.NewLine +
                           $"Diet Cokes cost {ConvertToCurrencyString(Price)}." + Environment.NewLine +
                           $"Your Change Due is {ConvertToCurrencyString(ChangeDue)}.",
            "Water" => "Would you like to purchase a Water?" + Environment.NewLine +
                       $"You have inserted {ConvertToCurrencyString(PaymentAmount)}." + Environment.NewLine +
                       $"Waters cost {ConvertToCurrencyString(Price)}." + Environment.NewLine +
                       $"Your Change Due is {ConvertToCurrencyString(ChangeDue)}.",
            "Lemonade" => "Would you like to purchase a Lemonade?" + Environment.NewLine +
                          $"You have inserted {ConvertToCurrencyString(PaymentAmount)}." + Environment.NewLine +
                          $"Lemonades cost {ConvertToCurrencyString(Price)}." + Environment.NewLine +
                          $"Your Change Due is {ConvertToCurrencyString(ChangeDue)}.",
            _ => "There was an error processing your request."
        };

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void Initialize(string drink, double paymentAmount, double price)
        {
            _paymentAmount = paymentAmount;
            _price = price;
            _changeDue = paymentAmount - price;
            _beverageSelected = drink;

            _backGroundImage = drink switch
            {
                "Coke" => UsefulExtensions.UsefulExtensions.ToImage(Resources.Coke_Round_Logo),
                "Diet Coke" => UsefulExtensions.UsefulExtensions.ToImage(Resources.Diet_Coke_Round_Logo),
                "Water" => UsefulExtensions.UsefulExtensions.ToImage(Resources.Water_Round_Logo),
                "Lemonade" => UsefulExtensions.UsefulExtensions.ToImage(Resources.Lemonade_Round_Logo),
                _ => UsefulExtensions.UsefulExtensions.ToImage(Resources.Transparent)
            };
        }

        private static string ConvertToCurrencyString(double amount)
        {
            return $"{Convert.ToDecimal(amount):C}";
        }
    }

    //public class MyMessageBox : MessageBox, INotifyPropertyChanged
    //{
    //    private BitmapImage _backGroundImage;
    //    private string _beverageSelected;
    //    private double _paymentAmount;
    //    private double _changeDue;
    //    private double _price;

    //    public double PaymentAmount
    //    {
    //        get => _paymentAmount;
    //        set
    //        {
    //            if (value.Equals(_paymentAmount)) return;
    //            _paymentAmount = value;
    //            OnPropertyChanged(nameof(PaymentAmount));
    //        }
    //    }

    //    public double ChangeDue
    //    {
    //        get => _changeDue;
    //        set
    //        {
    //            if (value.Equals(_changeDue)) return;
    //            _changeDue = value;
    //            OnPropertyChanged(nameof(ChangeDue));
    //        }
    //    }

    //    public string BeverageSelected
    //    {
    //        get => _beverageSelected;
    //        set
    //        {
    //            if (value.Equals(_beverageSelected)) return;
    //            _beverageSelected = value;
    //            OnPropertyChanged(nameof(BeverageSelected));
    //        }
    //    }

    //    public BitmapImage BackGroundImage
    //    {
    //        get => _backGroundImage;
    //        set
    //        {
    //            if (value.Equals(_backGroundImage)) return;
    //            _backGroundImage = value;
    //            OnPropertyChanged(nameof(BackGroundImage));
    //        }
    //    }

    //    public double Price
    //    {
    //        get => _price;
    //        set
    //        {
    //            if (value.Equals(_price)) return;
    //            _price = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public string ResponseText => BeverageSelected switch
    //    {
    //        "Coke" => $"Would you like to purchase a Coke? You have inserted {ConvertToCurrencyString(PaymentAmount)}. Cokes cost {ConvertToCurrencyString(Price)}. Your Change Due is {ConvertToCurrencyString(ChangeDue)}. ",
    //        "Diet Coke" => $"Would you like to purchase a Diet Coke? You have inserted {ConvertToCurrencyString(PaymentAmount)}. Diet Cokes cost {ConvertToCurrencyString(Price)}. Your Change Due is {ConvertToCurrencyString(ChangeDue)}. ",
    //        "Water" => $"Would you like to purchase a Water? You have inserted {ConvertToCurrencyString(PaymentAmount)}. Waters cost {ConvertToCurrencyString(Price)}. Your Change Due is {ConvertToCurrencyString(ChangeDue)}. ",,
    //        "Lemonade" => $"Would you like to purchase a Lemonade? You have inserted {ConvertToCurrencyString(PaymentAmount)}. Lemonades cost {ConvertToCurrencyString(Price)}. Your Change Due is {ConvertToCurrencyString(ChangeDue)}. ",
    //        _ => "There was an error processing your request."
    //    };

    //    public event PropertyChangedEventHandler? PropertyChanged;

    //    [NotifyPropertyChangedInvocator]
    //    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }

    //    public PurchaseViewModel()
    //    {

    //    }

    //    private static BitmapImage ToImage(byte[] array)
    //    {
    //        using var ms = new System.IO.MemoryStream(array);
    //        var image = new BitmapImage();
    //        image.BeginInit();
    //        image.CacheOption = BitmapCacheOption.OnLoad;
    //        image.StreamSource = ms;
    //        image.EndInit();
    //        return image;
    //    }

    //    public void Initialize(string drink, double paymentAmount, double price)
    //    {
    //        _paymentAmount = paymentAmount;
    //        _price = price;
    //        _changeDue = paymentAmount - price;
    //        _beverageSelected = drink;

    //        _backGroundImage = drink switch
    //        {
    //            "Coke" => ToImage(Properties.Resources.Coke_Round_Logo),
    //            "Diet Coke" => ToImage(Properties.Resources.Diet_Coke_Round_Logo),
    //            "Water" => ToImage(Properties.Resources.Water_Round_Logo),
    //            "Lemonade" => ToImage(Properties.Resources.Lemonade_Round_Logo),
    //            _ => ToImage(Properties.Resources.Transparent)
    //        };
    //    }

    //    private static string ConvertToCurrencyString(double amount)
    //    {
    //        return $"{Convert.ToDecimal(amount):C}";
    //    }
    //}
}
