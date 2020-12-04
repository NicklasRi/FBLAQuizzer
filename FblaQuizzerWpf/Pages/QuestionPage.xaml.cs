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
        private IEnumerable<QuizQuestionDisplay> quizQuestionsDisplay = null;
        private int questionIndex = 0;

        public QuestionPage(QuestionViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;

            quizQuestionsDisplay = QuizQuestionData.GetQuizQuestionsDisplay(viewModel.Quiz.Id);

            QuizQuestionDisplay firstQuizQuestion = quizQuestionsDisplay.First();

            this.LoadQuizQuestion(firstQuizQuestion.Id);

            this.LoadQuestion(firstQuizQuestion.QuestionId);
        }

        private void LoadQuestion(Guid id)
        {
            IQuestion question = QuestionData.GetQuestion(id);
            this.TopicLabel.Content = question.Topic;
            this.TextLabel.Content = question.Text;

            QuestionViewModel questionViewModel = (QuestionViewModel)this.DataContext;
            questionViewModel.Question = question;

            MultipleChoiceQuestion multipleChoiceQuestion = question as MultipleChoiceQuestion;
            if (multipleChoiceQuestion != null)
            {
                questionViewModel.MultipleChoiceOptions = multipleChoiceQuestion.Options;
            }

            MatchingQuestion matchingQuestion = question as MatchingQuestion;
            if (matchingQuestion != null)
            {
                LoadMatchingQuestion(matchingQuestion);
            }
        }

        private void LoadQuizQuestion(Guid quizQuestionId)
        {
            IQuizQuestion quizQuestion = QuizQuestionData.GetQuizQuestion(quizQuestionId);
            QuestionViewModel questionViewModel = (QuestionViewModel)this.DataContext;
            questionViewModel.QuizQuestion = quizQuestion;
            this.NumberLabel.Content = string.Format("Question {0}", quizQuestion.QuestionNumber);
        }

        private void LoadMatchingQuestion(MatchingQuestion matchingQuestion)
        {
            QuestionViewModel questionViewModel = (QuestionViewModel)this.DataContext;
       
            MatchingQuizQuestion matchingQuizQuestion = (MatchingQuizQuestion)questionViewModel.QuizQuestion;
            
            if(matchingQuizQuestion.MatchingAnswers == null)
            {
                List<MatchingAnswerDisplay> matchingAnswers = new List<MatchingAnswerDisplay>();

                for (int i = 0; i < matchingQuestion.Prompts.Count(); i++)
                {
                    MatchingAnswer matchingAnswer = new MatchingAnswer();
                    matchingAnswer.Id = Guid.NewGuid();
                    matchingAnswer.MatchingAnswerOptionId = matchingQuestion.Options.ElementAt(i).Id;
                    matchingAnswer.MatchingAnswerPromptId = matchingQuestion.Prompts.ElementAt(i).Id;
                }
            }
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;
            QuizQuestionDisplay quizQuestionDisplay = quizQuestionsDisplay.ElementAt(++questionIndex);
            this.LoadQuizQuestion(quizQuestionDisplay.Id);
            this.LoadQuestion(quizQuestionDisplay.QuestionId);
            this.EvaluateButtons(viewModel);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;
            QuizQuestionDisplay quizQuestionDisplay = quizQuestionsDisplay.ElementAt(--questionIndex);
            this.LoadQuizQuestion(quizQuestionDisplay.Id);
            this.LoadQuestion(quizQuestionDisplay.QuestionId);
            this.EvaluateButtons(viewModel);
        }

        private void EvaluateButtons(QuestionViewModel viewModel)
        {
            viewModel.IsLastQuestion = quizQuestionsDisplay.Count() - 1 == questionIndex;
            viewModel.IsFirstQuestion = questionIndex == 0;
        }
    }
}
