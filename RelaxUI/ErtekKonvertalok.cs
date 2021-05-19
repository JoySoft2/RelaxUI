using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace RelaxUI
{
    public class UzenetDobozIkonImageKonvertalo : IValueConverter
    {
        private static readonly BitmapImage[] ikonok = new BitmapImage[]
        {
            null,
            new BitmapImage(new Uri("pack://application:,,,/RelaxUI;component/Kepek/StatusOK_32x.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri("pack://application:,,,/RelaxUI;component/Kepek/StatusInformation_32x.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri("pack://application:,,,/RelaxUI;component/Kepek/StatusWarning_32x.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri("pack://application:,,,/RelaxUI;component/Kepek/StatusCriticalError_32x.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri("pack://application:,,,/RelaxUI;component/Kepek/StatusHelp_32x.png", UriKind.RelativeOrAbsolute))
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ikonok[(int)(UzenetDobozIkon)value];

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class BoolLathatosagKonvertalo : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == Visibility.Visible;
    }
}
