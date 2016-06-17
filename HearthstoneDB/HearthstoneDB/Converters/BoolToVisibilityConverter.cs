using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HearthstoneDB.Converters 
{
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string retour = "Visible";
            bool isVisible = (bool)value;

            if (!isVisible) retour = "Hidden";

            return retour;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "Visible")
                return true;
            else
                return false;
        }
    }
}
