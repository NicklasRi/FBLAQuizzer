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

namespace FblaQuizzerWpf.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public delegate void ClickEventHandler();

        public event ClickEventHandler ViewClicked;

        public event ClickEventHandler CreateClicked;
        
        public HomePage()
        {
            InitializeComponent();
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            ViewClicked();   
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateClicked();
        }
    }
}
