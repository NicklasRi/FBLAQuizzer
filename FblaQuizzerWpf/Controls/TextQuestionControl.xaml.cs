using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FblaQuizzerWpf.Controls
{
    /// <summary>
    /// Interaction logic for TextQuestionControl.xaml
    /// </summary>
    public partial class TextQuestionControl : UserControl
    {
        public TextQuestionControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AnswerProperty = DependencyProperty.
            Register("Answer", typeof(string), typeof(TextQuestionControl), new PropertyMetadata(null));

        public string Answer
        {
            get
            {
                return (string)this.GetValue(AnswerProperty);
            }

            set
            {
                this.SetValue(AnswerProperty, value);
            }
        }
    }
}
