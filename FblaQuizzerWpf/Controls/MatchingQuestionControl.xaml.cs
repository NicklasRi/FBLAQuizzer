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
using FblaQuizzerWpf.ViewModels;

namespace FblaQuizzerWpf.Controls
{
    /// <summary>
    /// Interaction logic for MatchingQuestionControl.xaml
    /// </summary>
    public partial class MatchingQuestionControl : UserControl
    {
        public MatchingQuestionControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MatchingAnswersProperty = DependencyProperty
            .Register("MatchingAnswers", typeof(IEnumerable<MatchingAnswerPrompt>), 
            typeof(MatchingQuestionControl), new PropertyMetadata(null));

        public IEnumerable<MatchingAnswerPrompt> MatchingAnswers
        {
            get
            {
                return (IEnumerable<MatchingAnswerPrompt>)this.GetValue(MatchingAnswersProperty);
            }

            set
            {
                this.SetValue(MatchingAnswersProperty, value);
            }
        }

        private void StackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                StackPanel panel = (StackPanel)sender;
                DataObject data = new DataObject();
                Label optionLabel = e.Source as Label;
                if(optionLabel != null) 
                {
                    MatchingAnswerOption option = (MatchingAnswerOption)optionLabel.DataContext;
                    data.SetData("OptionItem", option);
                    DragDrop.DoDragDrop(panel, data, DragDropEffects.Copy | DragDropEffects.Move);
                }
                
            }
        }

        private void OptionItems_Drop(object sender, DragEventArgs e)
        {
            FrameworkElement controlTarget = e.OriginalSource as FrameworkElement;
            if(controlTarget != null)
            {
                MatchingAnswerOption targetOption = (MatchingAnswerOption)controlTarget.DataContext;
                MatchingAnswerOption sourceOption = (MatchingAnswerOption)e.Data.GetData("OptionItem");
            }
            
        }
    }
}
