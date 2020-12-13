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

namespace FblaQuizzerWpf.Controls
{
    /// <summary>
    /// Interaction logic for MatchingQuestionControl.xaml
    /// </summary>
    public partial class MatchingQuestionControl : QuestionControl
    {
        public MatchingQuestionControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MatchingAnswersProperty = DependencyProperty
            .Register("MatchingAnswers", typeof(IEnumerable<MatchingAnswerDisplay>),
            typeof(MatchingQuestionControl), new PropertyMetadata(null));

        public IEnumerable<MatchingAnswerDisplay> MatchingAnswers
        {
            get
            {
                return (IEnumerable<MatchingAnswerDisplay>)this.GetValue(MatchingAnswersProperty);
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
                TextBlock optionLabel = e.Source as TextBlock;
                if (optionLabel != null)
                {
                    MatchingAnswerDisplay matchingAnswer = (MatchingAnswerDisplay)optionLabel.DataContext;
                    data.SetData("MatchingAnswerItem", matchingAnswer);
                    DragDrop.DoDragDrop(panel, data, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        private void OptionItems_Drop(object sender, DragEventArgs e)
        {
            FrameworkElement controlTarget = e.OriginalSource as FrameworkElement;
            if (controlTarget != null)
            {
                Guid targetOptionId = (Guid)controlTarget.Tag;

                MatchingAnswerDisplay source = (MatchingAnswerDisplay)e.Data.GetData("MatchingAnswerItem");

                IEnumerable<MatchingAnswer> matchingAnswers = ((MatchingQuizQuestion)this.QuizQuestion).MatchingAnswers;
                MatchingAnswer answerTarget = matchingAnswers.First(x => x.MatchingAnswerOptionId == targetOptionId);
                MatchingAnswer answerSource = matchingAnswers.First(x => x.MatchingAnswerOptionId == source.MatchingAnswerOptionId);

                Guid targetId = answerTarget.MatchingAnswerOptionId;

                answerTarget.MatchingAnswerOptionId = answerSource.MatchingAnswerOptionId;
                answerSource.MatchingAnswerOptionId = targetId;

                QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;
                MatchingQuestion question = (MatchingQuestion)viewModel.Question;

                ((MatchingQuizQuestion)this.QuizQuestion).Grade(question);

                QuizQuestionData.SaveQuizQuestion(this.QuizQuestion);

                this.MatchingAnswers = QuizQuestionData.GetMatchingAnswersDisplay(this.QuizQuestion.Id);
            }
        }
    }
}
