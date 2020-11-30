using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Shamanic.Views.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            try
            {
                return GetDescription((Enum)value);
            }
            catch (Exception)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Enum.ToObject(targetType, value);

        public static string GetDescription(Enum @enum)
        {
            var type = @enum.GetType();
            var info = type.GetMember(@enum.ToString());
            if (info.Length > 0)
            {
                var attributes = info[0].GetCustomAttributes(typeof(LocalizedAttribute), false);
                if (attributes.Length > 0)
                    return ((LocalizedAttribute)attributes[0]).Description;
                attributes = info[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                    return ((DescriptionAttribute)attributes[0]).Description;
            }
            return @enum.ToString();
        }
    }
}
