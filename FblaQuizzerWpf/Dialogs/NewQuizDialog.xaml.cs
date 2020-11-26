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
using System.Windows.Shapes;

namespace FblaQuizzerWpf.Dialogs
{
    /// <summary>
    /// Interaction logic for NewQuizDialog.xaml
    /// </summary>
    public partial class NewQuizDialog : Window
    {
        public NewQuizDialog()
        {
            InitializeComponent();
        }

        public string QuizName { get { return NameText.Text; } }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void NameText_KeyUp(object sender, KeyEventArgs e)
        {
            this.OkButton.IsEnabled = NameText.Text.Length > 0;
        }
    }
}
