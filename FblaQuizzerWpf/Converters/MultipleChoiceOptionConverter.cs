using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerWpf.Converters
{
    public class MultipleChoiceOptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MultipleChoiceOption option = (MultipleChoiceOption)value;
            return option.Letter + ". " + option.Text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
