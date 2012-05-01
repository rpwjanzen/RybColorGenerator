using System;
using System.Windows.Data;
using System.Windows.Media;

namespace ColorSpace
{
    public sealed class RybColorToBrushConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            RybColor rybColor = value as RybColor;
            if (rybColor == null)
                return new SolidColorBrush(Colors.Cyan);
            else
                return new SolidColorBrush(rybColor.ToColor());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
