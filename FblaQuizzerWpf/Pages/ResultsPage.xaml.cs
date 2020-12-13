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
using System.Xml;
using FblaQuizzerBusiness.Interfaces;
using FblaQuizzerBusiness.Reports;
using FblaQuizzerWpf.ViewModels;
using Microsoft.Win32;

namespace FblaQuizzerWpf.Pages
{
    /// <summary>
    /// Interaction logic for ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {
        public event EventHandler CloseClicked;

        public ResultsPage(ResultsViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseClicked(this, EventArgs.Empty);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            ResultsViewModel viewModel = (ResultsViewModel)this.DataContext;
            PrintQuiz(viewModel.Quiz.Id);
        }
    
        private void PrintQuiz(Guid quizId)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xml Files | *.xml";
            saveFileDialog.CreatePrompt = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                XmlDocument xml = QuizReport.CreateQuizReport(quizId);
                xml.Save(saveFileDialog.FileName);            
            }
        }
    }
}
