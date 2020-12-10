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
        private IEnumerable<QuizQuestionKey> quizQuestionsDisplay = null;
        private int questionIndex = 0;

        public QuestionPage(QuestionViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;

            quizQuestionsDisplay = QuizQuestionData.GetQuizQuestionKeys(viewModel.Quiz.Id);

            QuizQuestionKey firstQuizQuestion = quizQuestionsDisplay.First();

            this.LoadQuestion(firstQuizQuestion.QuestionId);
            this.LoadQuizQuestion(firstQuizQuestion.Id);
        }

        private void LoadQuestion(Guid id)
        {
            IQuestion question = QuestionData.GetQuestion(id);
            this.TopicLabel.Content = question.Topic;
            this.TextLabel.Content = question.Text;

            QuestionViewModel questionViewModel = (QuestionViewModel)this.DataContext;
            questionViewModel.IsMultipleChoiceQuestion = question.QuestionType == QuestionType.MultipleChoice;
            questionViewModel.IsTrueFalseQuestion = question.QuestionType == QuestionType.TrueFalse;
            questionViewModel.IsTextQuestion = question.QuestionType == QuestionType.Text;
            questionViewModel.IsMatchingQuestion = question.QuestionType == QuestionType.Matching;
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
            
            if(matchingQuizQuestion.MatchingAnswers.Count() == 0)
            {
                List<MatchingAnswer> matchingAnswers = new List<MatchingAnswer>();

                for (int i = 0; i < matchingQuestion.Prompts.Count(); i++)
                {
                    MatchingAnswer matchingAnswer = new MatchingAnswer();
                    matchingAnswer.MatchingAnswerOptionId = matchingQuestion.Options.ElementAt(i).Id;
                    matchingAnswer.MatchingAnswerPromptId = matchingQuestion.Prompts.ElementAt(i).Id;
                    
                    matchingAnswers.Add(matchingAnswer);
                }
                matchingQuizQuestion.MatchingAnswers = matchingAnswers;
                QuizQuestionData.SaveQuizQuestion(matchingQuizQuestion);
            }

            questionViewModel.MatchingAnswers = QuizQuestionData.GetMatchingAnswersDisplay(matchingQuizQuestion.Id);
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;
            QuizQuestionKey quizQuestionDisplay = quizQuestionsDisplay.ElementAt(++questionIndex);
            this.LoadQuizQuestion(quizQuestionDisplay.Id);
            this.LoadQuestion(quizQuestionDisplay.QuestionId);
            this.EvaluateButtons(viewModel);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;
            QuizQuestionKey quizQuestionDisplay = quizQuestionsDisplay.ElementAt(--questionIndex);
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
