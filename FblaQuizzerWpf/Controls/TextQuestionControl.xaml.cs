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
    /// Interaction logic for TextQuestionControl.xaml
    /// </summary>
    public partial class TextQuestionControl : QuestionControl
    {
        public TextQuestionControl()
        {
            InitializeComponent();
        }

        private void AnswerText_LostFocus(object sender, RoutedEventArgs e)
        {
            var binding = (sender as TextBox).GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();


            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;
            TextQuestion question = (TextQuestion)viewModel.Question;
            TextQuizQuestion quizQuestion = (TextQuizQuestion)this.QuizQuestion;

            if(quizQuestion.Answer != null)
            {
                string strippedAnswer = quizQuestion.Answer.Replace(" ", string.Empty);

                string strippedAnswerKey = question.Answer.Replace(" ", string.Empty);

                this.QuizQuestion.Correct = strippedAnswer.Equals(strippedAnswerKey, StringComparison.OrdinalIgnoreCase);
            }

            QuizQuestionData.SaveQuizQuestion(viewModel.QuizQuestion);
        }
    }
}
