using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KlasykaGatunku.Utils
{
    public class WindowStateToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowState windowState)
            {
                if (windowState == WindowState.Maximized)
                {
                    return "pack://application:,,,/Img/shorten_window_button.png";
                }
                else
                {
                    return "pack://application:,,,/Img/expand_window_button.png";
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
