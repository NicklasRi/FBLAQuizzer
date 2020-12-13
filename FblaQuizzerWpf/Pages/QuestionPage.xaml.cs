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
using FblaQuizzerData.Data;
using FblaQuizzerWpf.ViewModels;

namespace FblaQuizzerWpf.Pages
{
    /// <summary>
    /// Interaction logic for QuestionPage.xaml
    /// </summary>
    public partial class QuestionPage : Page
    {
        public event EventHandler FinishClicked;

        private IEnumerable<QuizQuestionKey> questionIds = null;
        private int questionIndex = 0;

        public QuestionPage(QuestionViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;

            questionIds = QuizQuestionData.GetQuizQuestionKeys(viewModel.Quiz.Id);

            QuizQuestionKey firstQuizQuestion = questionIds.First();

            this.LoadQuizQuestion(firstQuizQuestion.Id);
            this.LoadQuestion(firstQuizQuestion.QuestionId);
            
        }

        private void LoadQuestion(Guid id)
        {
            IQuestion question = QuestionData.GetQuestion(id);
            this.TopicLabel.Text = question.Topic;
            this.TextLabel.Text = question.Text;

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
            this.NumberLabel.Text = string.Format("Question {0}", quizQuestion.QuestionNumber);
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

            //users might not make changes to a matching question, but
            //we need to save it for grading purposes
            SaveQuestion(viewModel.QuizQuestion, viewModel.Question);
            
            QuizQuestionKey quizQuestionDisplay = questionIds.ElementAt(++questionIndex);
            this.LoadQuizQuestion(quizQuestionDisplay.Id);
            this.LoadQuestion(quizQuestionDisplay.QuestionId);
            this.EvaluateButtons(viewModel);
            
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;

            SaveQuestion(viewModel.QuizQuestion, viewModel.Question);

            QuizQuestionKey quizQuestionDisplay = questionIds.ElementAt(--questionIndex);
            this.LoadQuizQuestion(quizQuestionDisplay.Id);
            this.LoadQuestion(quizQuestionDisplay.QuestionId);
            this.EvaluateButtons(viewModel);
        }

        private void EvaluateButtons(QuestionViewModel viewModel)
        {
            viewModel.IsLastQuestion = questionIds.Count() - 1 == questionIndex;
            viewModel.IsFirstQuestion = questionIndex == 0;
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;

            SaveQuestion(viewModel.QuizQuestion, viewModel.Question);

            Grade();

            FinishClicked(this, EventArgs.Empty);
        }

        private void Grade()
        {
            int correctCounter = 0;

            foreach (QuizQuestionKey key in questionIds)
            {
                IQuizQuestion quizQuestion = QuizQuestionData.GetQuizQuestion(key.Id);

                if (quizQuestion.Correct.HasValue && quizQuestion.Correct.Value)
                {
                    correctCounter++;
                }
            }

            QuestionViewModel viewModel = (QuestionViewModel)this.DataContext;

            viewModel.Quiz.Score = ((decimal)correctCounter / questionIds.Count()) * 100;
            QuizData.SaveQuiz(viewModel.Quiz);
        }

        private static void SaveQuestion(IQuizQuestion quizQuestion, IQuestion question)
        {
            MatchingQuizQuestion matchingQuizQuestion = quizQuestion as MatchingQuizQuestion;

            if(matchingQuizQuestion != null)
            {
                matchingQuizQuestion.Grade((MatchingQuestion)question);
            }

            QuizQuestionData.SaveQuizQuestion(quizQuestion);
        }
    }
}
