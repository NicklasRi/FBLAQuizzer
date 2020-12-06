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
using FblaQuizzerBusiness.Data;
using FblaQuizzerBusiness.Models;
using FblaQuizzerWpf.CustomControls;

namespace FblaQuizzerWpf.Controls
{
    /// <summary>
    /// Interaction logic for MultipleChoiceQuestionControl.xaml
    /// </summary>
    public partial class MultipleChoiceQuestionControl : QuestionControl
    {
        public MultipleChoiceQuestionControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty OptionsProperty = DependencyProperty
            .Register("Options",
            typeof(IEnumerable<MultipleChoiceOption>),
            typeof(MultipleChoiceQuestionControl),
            new PropertyMetadata(null));

        public IEnumerable<MultipleChoiceOption> Options
        {
            get
            {
                return (IEnumerable<MultipleChoiceOption>)this.GetValue(OptionsProperty);
            }

            set
            {
                this.SetValue(OptionsProperty, value);
            }
        }

        public static readonly DependencyProperty AnswerProperty =
            DependencyProperty.Register
            ("Answer", typeof(Guid), typeof(MultipleChoiceQuestionControl), new PropertyMetadata(null));
        
        public Guid Answer
        {
            get
            {
                return (Guid)this.GetValue(AnswerProperty);
            }

            set
            {
                this.SetValue(AnswerProperty, value);
            }
        }

        private void MultipleChoiceRadioButton_Click(object sender, RoutedEventArgs e)
        {
            MultipleChoiceRadioButton radioButton = (MultipleChoiceRadioButton)e.Source;
            MultipleChoiceOption answer = (MultipleChoiceOption)radioButton.DataContext;
            ((MultipleChoiceQuizQuestion)this.QuizQuestion).Answer = answer.Id;

            QuizQuestionData.SaveQuizQuestion(this.QuizQuestion);
        }
    }
}
