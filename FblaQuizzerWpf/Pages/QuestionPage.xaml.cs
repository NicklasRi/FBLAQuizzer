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
using FblaQuizzerBusiness.Interfaces;
using FblaQuizzerBusiness.Models;
using FblaQuizzerWpf.ViewModels;

namespace FblaQuizzerWpf.Pages
{
    /// <summary>
    /// Interaction logic for QuestionPage.xaml
    /// </summary>
    public partial class QuestionPage : Page
    {
        public QuestionPage(QuestionViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;

            IQuizQuestion quizQuestion = viewModel.Quiz.Questions.First();

            this.LoadQuizQuestion(quizQuestion);

            this.LoadQuestion(quizQuestion.QuestionId);
        }

        private void LoadQuestion(Guid id)
        {
            IQuestion question = QuestionData.GetQuestion(id);
            ((QuestionViewModel)this.DataContext).Question = question;
        }

        private void LoadQuizQuestion(IQuizQuestion quizQuestion)
        {
            this.NumberLabel.Content = string.Format("Question {0}", quizQuestion.QuestionNumber);
            this.TopicLabel.Content = quizQuestion.Topic;
            this.TextLabel.Content = quizQuestion.Text;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;
            IQuizQuestion quizQuestion = viewModel.Quiz.Questions.ElementAt(++viewModel.QuestionIndex);
            this.LoadQuizQuestion(quizQuestion);
            this.LoadQuestion(quizQuestion.QuestionId);
            this.EvaluateButtons(viewModel);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;
            IQuizQuestion quizQuestion = viewModel.Quiz.Questions.ElementAt(--viewModel.QuestionIndex);
            this.LoadQuizQuestion(quizQuestion);
            this.LoadQuestion(quizQuestion.QuestionId);
            this.EvaluateButtons(viewModel);
        }

        private void EvaluateButtons(QuestionViewModel viewModel)
        {
            viewModel.IsLastQuestion = viewModel.Quiz.Questions.Count() - 1 == viewModel.QuestionIndex;
            viewModel.IsFirstQuestion = viewModel.QuestionIndex == 0;
        }
    }
}
