using System;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;

namespace Software_Design_Examples.View_Model.UsefulExtensions;

public static class UsefulExtensions
{
    public enum MessageBoxType
    {
        ConfirmationWithYesNo = 0,
        ConfirmationWithYesNoCancel,
        Information,
        Error,
        Warning
    }

    public enum MessageBoxImage
    {
        Warning = 0,
        Question,
        Information,
        Error,
        None
    }
    public enum PropertyTypes
    {
        @int, @double, @string, @bool, error
    }

    public static PropertyTypes GetPropertyType(string? propertyName)
    {
        switch (propertyName)
        {
            case nameof(MainViewModel.ChangeDue) or nameof(MainViewModel.PaymentAmount) or nameof(MainViewModel.AvailableChange):
                return PropertyTypes.@double;
            case nameof(MainViewModel.CokeAmount) or nameof(MainViewModel.DietCokeAmount)
                or nameof(MainViewModel.WaterAmount)
                or nameof(MainViewModel.LemonadeAmount):
                return PropertyTypes.@int;
        }

        return PropertyTypes.error;
    }
    public static string ToPriceString(this double input)
    {
        return $"${Math.Round(Convert.ToDecimal(input.ToString(CultureInfo.CurrentCulture)), decimals: 2)}";
    }

    public static bool IsCreditCardInfoValid(this string cardNo, string expiryDate, string cvv)
    {
        var cardCheck = new Regex(@"^(1298|1267|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");
        var monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
        var yearCheck = new Regex(@"^20[0-9]{2}$");
        var cvvCheck = new Regex(@"^\d{3}$");

        if (!cardCheck.IsMatch(cardNo)) // <1>check card number is valid
            return false;
        if (!cvvCheck.IsMatch(cvv)) // <2>check cvv is valid as "999"
            return false;

        var dateParts = expiryDate.Split('/'); //expiry date in from MM/yyyy            
        if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1])) // <3 - 6>
            return false; // ^ check date format is valid as "MM/yyyy"

        var year = int.Parse(dateParts[1]);
        var month = int.Parse(dateParts[0]);
        var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month); //get actual expiry date
        var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

        //check expiry greater than today & within next 6 years <7, 8>>
        return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(6));
    }

    public static BitmapImage ToImage(byte[] array)
    {
        using var ms = new MemoryStream(array);
        var image = new BitmapImage();
        image.BeginInit();
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.StreamSource = ms;
        image.EndInit();
        return image;
    }
    public static string GetNameOf<T>(Expression<Func<T>> property)
    {
        var temp = (property.Body as MemberExpression)?.Member.Name;

        if (temp.Contains("Diet"))
        {
            return $"Resources/Diet Coke Round Logo.png";
        }

        if (temp.Contains("Coke"))
        {
            return $"Resources/Coke Round Logo.png";
        }

        if(temp.Contains("Water"))
        {
            return $"Resources/Water Round Logo.jpg";
        }

        if(temp.Contains("Lemonade"))
        {
            return $"Resources/Lemonade Round Logo.jpg";
        }

        return $"Resources/Transparent.jpg";
    }
}