using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Software_Design_Examples.Views
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DetermineBestConversionMethod(value);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DetermineBestConversionMethodBack(value);
        }

        private static object DetermineBestConversionMethod(object value)
        {
            return value switch
            {
                bool valueAsBool => valueAsBool == false ? Visibility.Hidden : Visibility.Visible,
                int valueAsInt => valueAsInt == 0 ? Visibility.Hidden : Visibility.Visible,
                _ => Visibility.Hidden
            };
        }

        private static object DetermineBestConversionMethodBack(object value)
        {
            return value switch
            {
                bool valueAsBool => valueAsBool ? Visibility.Visible : Visibility.Hidden,
                int valueAsInt => valueAsInt != 0 ? Visibility.Visible : Visibility.Hidden,
                _ => Visibility.Hidden
            };
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public BoolToVisibilityConverter()
        {
            // set defaults
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value is not bool isVisible)
                return FalseValue;
            return isVisible ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return FalseValue;
        }
    }
}
