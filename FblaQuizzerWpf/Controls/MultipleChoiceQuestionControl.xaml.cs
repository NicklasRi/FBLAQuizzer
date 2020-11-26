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
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerWpf.Controls
{
    /// <summary>
    /// Interaction logic for MultipleChoiceQuestionControl.xaml
    /// </summary>
    public partial class MultipleChoiceQuestionControl : UserControl
    {
        public MultipleChoiceQuestionControl()
        {
            InitializeComponent();
        }

        public DependencyProperty OptionsProperty = DependencyProperty
            .Register("Options",
            typeof(IEnumerable<MultipleChoiceOption>),
            typeof(MultipleChoiceQuestionControl),
            new PropertyMetadata());

        public IEnumerable<MultipleChoiceOption> Options { get; set; }
    }
}
