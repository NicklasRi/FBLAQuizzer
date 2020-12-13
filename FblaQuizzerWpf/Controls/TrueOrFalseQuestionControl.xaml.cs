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
using FblaQuizzerWpf.ViewModels;

namespace FblaQuizzerWpf.Controls
{
    /// <summary>
    /// Interaction logic for TrueOrFalseQuestionControl.xaml
    /// </summary>
    public partial class TrueOrFalseQuestionControl : QuestionControl
    {
        public TrueOrFalseQuestionControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AnswerProperty = 
            DependencyProperty.Register
            ("Answer", typeof(bool?),typeof(TrueOrFalseQuestionControl), new PropertyMetadata(null));

        public bool? Answer
        {
            get
            {
                return (bool?)this.GetValue(AnswerProperty);
            }

            set
            {
                this.SetValue(AnswerProperty, value);
            }
        }

        private void TrueButton_Click(object sender, RoutedEventArgs e)
        {
            SaveQuizQuestion();
        }

        private void FalseButton_Click(object sender, RoutedEventArgs e)
        {
            SaveQuizQuestion();
        }

        private void SaveQuizQuestion()
        {
            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;
            TrueFalseQuestion question = (TrueFalseQuestion)viewModel.Question;


            this.QuizQuestion.Correct = ((TrueFalseQuizQuestion)this.QuizQuestion).Answer == question.Answer;

            QuizQuestionData.SaveQuizQuestion(this.QuizQuestion);
        }
    }
}
