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
    /// Interaction logic for TrueOrFalseQuestionControl.xaml
    /// </summary>
    public partial class TrueOrFalseQuestionControl : UserControl
    {
        public TrueOrFalseQuestionControl()
        {
            InitializeComponent();
        }

        public bool Answer { get; set; }

        public static readonly DependencyProperty AnswerProperty = 
            DependencyProperty.Register
            ("Answer", typeof(bool),typeof(TrueOrFalseQuestionControl), new PropertyMetadata(null));
    }
}
