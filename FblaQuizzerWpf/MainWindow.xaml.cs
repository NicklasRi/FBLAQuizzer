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
using FblaQuizzerBusiness;
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

        public MainWindow()
        {
            InitializeComponent();

            HomePage homePage = new HomePage();
            homePage.ViewClicked += HomePage_ViewClicked;
            homePage.CreateClicked += HomePage_CreateClicked;
            MainFrame.Navigate(homePage);
        }

        private void GetQuizName()
        {
            
            
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
            QuizzesPage quizzesPage = new QuizzesPage(viewModel);
            MainFrame.Navigate(quizzesPage);
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void CreateQuiz(string name)
        {
            quiz = Quiz.CreateQuiz(name);
            QuestionViewModel viewModel = new QuestionViewModel(quiz);
            QuestionPage questionPage = new QuestionPage(viewModel);
            MainFrame.Navigate(questionPage);
        }
    }
}
