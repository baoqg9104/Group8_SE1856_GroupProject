using System;
using System.Globalization;
using System.Windows.Data;

namespace Group8_WPF
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int gender)
            {
                return gender == 1 ? "Male" : "Female";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string genderString)
            {
                return genderString == "Male" ? 1 : 0;
            }
            return Binding.DoNothing;
        }
    }

}