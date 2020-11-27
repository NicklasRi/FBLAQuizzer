using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerWpf.Converters
{
    class QuestionTypeToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            QuestionType questionTypeNeeded = (QuestionType)parameter;
            QuestionType questionTypeGiven = (QuestionType)value;
            return questionTypeGiven == questionTypeNeeded ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
