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
using FblaQuizzerWpf.Events;
using FblaQuizzerWpf.ViewModels;

namespace FblaQuizzerWpf.Pages
{
    /// <summary>
    /// Interaction logic for QuizzesPage.xaml
    /// </summary>
    public partial class QuizzesPage : Page
    {
        public delegate void QuizClickedDelegate(object sender, QuizClickedArgs args);

        public event QuizClickedDelegate QuizClicked;

        public QuizzesPage(QuizzesViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;
        }

        private void QuizzesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            QuizDisplay quiz = (QuizDisplay)this.QuizzesList.SelectedItem;
            QuizClicked(this, new QuizClickedArgs(quiz.Id));
        }
    }
}
