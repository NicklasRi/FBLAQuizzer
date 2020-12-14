using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using FblaQuizzerBusiness;
using FblaQuizzerBusiness.Interfaces;
using FblaQuizzerBusiness.Models;
using FblaQuizzerData.Data;
using FblaQuizzerWpf.Dialogs;
using FblaQuizzerWpf.Pages;
using FblaQuizzerWpf.ViewModels;

namespace FblaQuizzerWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Quiz quiz;
        private HomePage homePage;
        private QuizzesPage quizzesPage;

        public MainWindow()
        {
            InitializeComponent();

            GoToHomePage();
        }

        private void HomePage_CreateClicked()
        {
            NewQuizDialog dialog = new NewQuizDialog();
            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                string name = dialog.QuizName;
                this.CreateQuiz(name);
            }
        }

        private void HomePage_ViewClicked()
        {
            IEnumerable<QuizDisplay> quizzes = QuizData.GetAll();
            QuizzesViewModel viewModel = new QuizzesViewModel();
            viewModel.Quizzes = quizzes;
            if(this.quizzesPage == null)
            {
                this.quizzesPage = new QuizzesPage(viewModel);
                this.quizzesPage.QuizClicked += QuizzesPage_QuizClicked;
                this.quizzesPage.BackClicked += QuizzesPage_BackClicked;
            }
            MainFrame.Navigate(quizzesPage);
        }

        private void QuizzesPage_BackClicked(object sender, EventArgs e)
        {
            GoToHomePage();
        }

        private void QuizzesPage_QuizClicked(object sender, Events.QuizClickedArgs args)
        {
            LaunchResultsPage(args.QuizId);
        }

        private void CreateQuiz(string name)
        {
            quiz = Quiz.CreateQuiz(name);
            QuestionViewModel viewModel = new QuestionViewModel(quiz);
            QuestionPage questionPage = new QuestionPage(viewModel);
            questionPage.FinishClicked += QuestionPage_FinishClicked;
            MainFrame.Navigate(questionPage);
        }

        private void QuestionPage_FinishClicked(object sender, EventArgs e)
        {
            LaunchResultsPage(this.quiz.Id);
        }

        private void LaunchResultsPage(Guid quizId)
        {
            this.quiz = QuizData.GetQuiz(quizId);
            IEnumerable<IQuizQuestionResult> results = QuizData.GetResults(quizId);

            ResultsViewModel viewModel = new ResultsViewModel(quiz, results);
            ResultsPage resultsPage = new ResultsPage(viewModel);
            resultsPage.CloseClicked += ResultsPage_CloseClicked;
            MainFrame.Navigate(resultsPage);
        }

        private void ResultsPage_CloseClicked(object sender, EventArgs e)
        {
            GoToHomePage();
        }

        private void GoToHomePage()
        {
            if (this.homePage == null)
            {
                this.homePage = new HomePage();
                this.homePage.CreateClicked += HomePage_CreateClicked;
                this.homePage.ViewClicked += HomePage_ViewClicked;
            }

            MainFrame.Navigate(homePage);
        }
    }
}
