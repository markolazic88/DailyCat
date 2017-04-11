namespace DailyCat.View.Converters
{
    using System;
    using System.Globalization;

    using Xamarin.Forms;

    public class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {

                return !(bool)value;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {

                return !(bool)value;
            }

            return value;
        }
    }
}
