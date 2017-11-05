using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF_MVVM.Converters
{
    public class TextBoxValueAppenderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TextBox tB = values[0] as TextBox;
            String valueToAdd = values[1] as String;
            String result = String.Empty;

            if (tB != null && valueToAdd != null)
            {
                if (valueToAdd.Contains("CLRSCR"))
                {
                    result = String.Empty;
                }
                else
                {
                    result = tB.Text + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture) + ": " + valueToAdd + Environment.NewLine;
                }
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
