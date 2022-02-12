using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Software_Design_Examples.Models.Beverages;
using Software_Design_Examples.View_Model;
using Software_Design_Examples.View_Model.Read_Data_From_File;
using Software_Design_Examples.View_Model.UsefulExtensions;
using Software_Design_Examples.View_Model.Write_Data_to_File;
using Software_Design_Examples.Views;

namespace Software_Design_Examples;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Properties

    public string ConfigurationFileLocation { get; private set; }
    private MainViewModel _viewModel { get; init; }
    private InventoryAndLedgerSingleton instance = InventoryAndLedgerSingleton.Instance;
    private DoubleAnimation imgAnimation;


    #endregion

    #region Constructor

    public MainWindow()
    {
        ConfigurationFileLocation = Properties.Resources.Config2;
        InitializeComponent();
        _viewModel = (MainViewModel)Resources["MainViewModel"];
        PaymentStackPanel.DataContext = _viewModel;
        Products.DataContext = _viewModel;
        Products.ItemsSource = _viewModel.VendingList.VendingItems;
        InventoryDockPanel.DataContext = _viewModel;
        _viewModel.PropertyChanged += MyViewModel_PropertyChanged;
    }

    #endregion

    #region Methods

    #region Payment Methods

    private void AddQuarter_ToPayment(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => _viewModel.AddPayment(0.25));
        UpdateDataContext();
    }

    private void Add_HalfDollar_ToPayment(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => _viewModel.AddPayment(0.50));
        UpdateDataContext();
    }

    private void Add_Dollar_ToPayment(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() => _viewModel.AddPayment(1));
        UpdateDataContext();
    }

    #endregion

    #region Property Changed Methods

    #region Generics

    private static double? GetNew_Double_PropertyValue(object sender, PropertyChangedEventArgs e, out PropertyInfo? propertyToUpdate)
    {
        var temp = sender as MainViewModel;
        var property = temp!.GetType().GetProperty(e.PropertyName ?? string.Empty) ?? null;
        propertyToUpdate = property ?? null;
        return (property == null) ? null : (double)(property.GetValue(temp) ?? null)!;
    }

    private static int? GetNew_Int_PropertyValue(object sender, PropertyChangedEventArgs e, out PropertyInfo? propertyToUpdate)
    {
        var temp = sender as MainViewModel;
        var property = temp!.GetType().GetProperty(e.PropertyName ?? string.Empty) ?? null;
        propertyToUpdate = property ?? null;
        return (property == null) ? null : (int)(property.GetValue(temp) ?? null)!;
    }

    private void Invoke_Double_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var newValue = GetNew_Double_PropertyValue(sender, e, out var property);

        Dispatcher.Invoke(() =>
        {
            if (newValue == null) return;

            _viewModel.GetType().GetProperty(property?.Name!)?.SetValue(_viewModel, Convert.ToDouble(newValue));
            UpdateDataContext();
        });
    }

    private void Invoke_Int_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var newValue = GetNew_Int_PropertyValue(sender, e, out var property);

        Dispatcher.Invoke(() =>
        {
            if (newValue == null) return;

            _viewModel.GetType().GetProperty(property?.Name!)?.SetValue(_viewModel, Convert.ToInt32(newValue));
            UpdateDataContext();
        });
    }

    #endregion

    private void MyViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender == null) return;
        switch (UsefulExtensions.GetPropertyType(e.PropertyName))
        {
            case UsefulExtensions.PropertyTypes.@double:
                {
                    Invoke_Double_PropertyChanged(sender, e);
                    break;
                }
            case UsefulExtensions.PropertyTypes.@int:
                {
                    Invoke_Int_PropertyChanged(sender, e);
                    break;
                }
            case UsefulExtensions.PropertyTypes.@string:
            case UsefulExtensions.PropertyTypes.@bool:
            case UsefulExtensions.PropertyTypes.error:
            default: return;
        }
    }

    #endregion

    private void PurchaseBeverage(object sender, RoutedEventArgs e)
    {
        #region Constants

        const string COKE = "Coke";
        const string DIET_COKE = "Diet Coke";
        const string WATER = "Water";
        const string LEMONADE = "Lemonade";

        #endregion
        var vendingItem = (VendingItem)(sender as Button)?.Tag!;
        var provider = new NumberFormatInfo();
        provider.NumberDecimalSeparator = ".";
        provider.CurrencySymbol = "$";
        var price = double.Parse(vendingItem.Price, NumberStyles.Currency);
        if (_viewModel.PaymentAmount < price) return;

        var purchase = new PurchaseViewModel();

        switch (vendingItem.Name)
        {
            case COKE:
                {
                    var beverage = new Coke { Name = vendingItem.Name, Price = price };
                    Dispatcher.Invoke(_viewModel.PurchaseBeverage(beverage));
                    purchase.Initialize(COKE, _viewModel.PaymentAmount, price);
                    _viewModel.CokeAmount--;
                    InventoryAndLedgerSingleton.Instance.BeverageInventory.NumberOfCokesInStock--;
                    break;
                }
            case DIET_COKE:
                {
                    var beverage = new Diet_Coke { Name = vendingItem.Name, Price = price };
                    Dispatcher.Invoke(_viewModel.PurchaseBeverage(beverage));
                    purchase.Initialize(DIET_COKE, _viewModel.PaymentAmount, price);
                    _viewModel.DietCokeAmount--;
                    InventoryAndLedgerSingleton.Instance.BeverageInventory.NumberOfDietCokesInStock--;
                    break;
                }
            case WATER:
                {
                    var beverage = new Water { Name = vendingItem.Name, Price = price };
                    Dispatcher.Invoke(_viewModel.PurchaseBeverage(beverage));
                    purchase.Initialize(WATER, _viewModel.PaymentAmount, price);
                    _viewModel.WaterAmount--;
                    InventoryAndLedgerSingleton.Instance.BeverageInventory.NumberOfWatersInStock--;
                    break;
                }
            case LEMONADE:
                {
                    var beverage = new Lemonade { Name = vendingItem.Name, Price = price };
                    Dispatcher.Invoke(_viewModel.PurchaseBeverage(beverage));
                    purchase.Initialize(LEMONADE, _viewModel.PaymentAmount, price);
                    _viewModel.LemonadeAmount--;
                    InventoryAndLedgerSingleton.Instance.BeverageInventory.NumberOfLemonadesInStock--;
                    break;
                }
            default:
                throw new ArgumentOutOfRangeException();
        }

        DisplayConfirmationWindow(purchase);
    }

    private void DisplayConfirmationWindow(PurchaseViewModel? vm)
    {
        var messageBoxResult =
            WpfMessageBox.Show("Confirm your selection.", vm.ResponseText,  MessageBoxButton.YesNo, MessageBoxImage.Question, vm.BackGroundImage, ref vm);
        if (messageBoxResult != MessageBoxResult.Yes) return;
        DispenseChange();
        UpdateConfigFile();
    }

    private void UpdateConfigFile()
    {
        var assembly = Assembly.GetExecutingAssembly();
        string resourceName = assembly.GetManifestResourceNames()
            .Single(str => str.EndsWith("Config.txt"));
        var temp = new WriteDataToFile(resourceName);
    }

    private void UpdateDataContext()
    {
        PaymentStackPanel.DataContext = _viewModel;
        InventoryDockPanel.DataContext = _viewModel;
        Products.DataContext = _viewModel;
    }

    #endregion

    private void Refill_Beverages(object sender, RoutedEventArgs e)
    {
        if (instance.CokeInventoryIsNotAtMaximum)
        {
            _viewModel.CokeAmount = instance.BeverageInventory.MaxNumberOfCokesAvailable;
        }

        if (instance.DietCokeInventoryIsNotAtMaximum)
        {
            _viewModel.DietCokeAmount = instance.BeverageInventory.MaxNumberOfDietCokesAvailable;
        }

        if (instance.WaterInventoryIsNotAtMaximum)
        {
            _viewModel.WaterAmount = instance.BeverageInventory.MaxNumberOfWatersAvailable;
        }

        if (instance.LemonadeInventoryIsNotAtMaximum)
        {
            _viewModel.LemonadeAmount = instance.BeverageInventory.MaxNumberOfLemonadesAvailable;
        }

        UpdateConfigFile();
    }

    private void WithdrawPayment(object sender, RoutedEventArgs e)
    {
        _viewModel.CashInMachine = instance.CashLedger.MinCashInMachine;
    }

    private void DispenseChange()
    {
        _viewModel.ChangeDue = 0;
        _viewModel.PaymentAmount = 0;
    }

    private void EnterManagerMode_OnClick(object sender, RoutedEventArgs e)
    {
        Dispatcher.Invoke(() =>
        {
            _viewModel.InManagerMode = true;
            ExitManagerMode.Visibility = Visibility.Visible;
            EnterManagerMode.Visibility = Visibility.Hidden;
            ManagerTicketResolution.Visibility = Visibility.Visible;
        });
    }

    private void ExitManagerMode_OnClick(object sender, RoutedEventArgs e)
    {
        
        Dispatcher.Invoke(() =>
        {
            _viewModel.InManagerMode = false;
            EnterManagerMode.Visibility = Visibility.Visible;
            ExitManagerMode.Visibility = Visibility.Hidden;
            ManagerTicketResolution.Visibility = Visibility.Hidden;
        });
    }


}