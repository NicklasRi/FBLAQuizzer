using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FblaQuizzerBusiness.Interfaces;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerWpf.ViewModels
{
    public class QuestionViewModel:ViewModelBase
    {   
        public QuestionViewModel(Quiz quiz)
        {
            this.Quiz = quiz;
        }

        public Quiz Quiz { get; }

        private IQuestion question;

        public IQuestion Question
        {
            get
            {
                return question;
            }

            set
            {
                question = value;
                OnPropertyChanged(nameof(Question));
            }
        }

        private IQuizQuestion quizQuestion;

        public IQuizQuestion QuizQuestion
        {
            get
            {
                return quizQuestion;
            }

            set
            {
                quizQuestion = value;
                OnPropertyChanged(nameof(QuizQuestion));
            }
        }

        private bool isFirstQuestion = true;

        public bool IsFirstQuestion
        {
            get
            {
                return isFirstQuestion;
            }
            set
            {
                isFirstQuestion = value;
                OnPropertyChanged(nameof(IsFirstQuestion));
            }
        }

        private bool isLastQuestion = false;
        public bool IsLastQuestion {
            get
            {
                return isLastQuestion;
            }

            set
            {
                isLastQuestion = value;
                OnPropertyChanged(nameof(IsLastQuestion));
            }
        }

        

        private IEnumerable<MultipleChoiceOption> multipleChoiceOptions;

        public IEnumerable<MultipleChoiceOption> MultipleChoiceOptions
        {
            
            get
            {
                return multipleChoiceOptions;
            }

            set
            {
                multipleChoiceOptions = value;
                OnPropertyChanged(nameof(MultipleChoiceOptions));
            }
        }

        private IEnumerable<MatchingAnswerDisplay> matchingAnswers;

        public IEnumerable<MatchingAnswerDisplay> MatchingAnswers
        {
            get
            {
                return matchingAnswers;
            }

            set
            {
                matchingAnswers = value;
                OnPropertyChanged(nameof(MatchingAnswers));
            }
        }

        private bool isMultipleChoiceQuestion;

        public bool IsMultipleChoiceQuestion
        {
            get
            {
                return isMultipleChoiceQuestion;
            }

            set
            {
                isMultipleChoiceQuestion = value;
                OnPropertyChanged(nameof(IsMultipleChoiceQuestion));
            }
        }

        private bool isTrueFalseQuestion;

        public bool IsTrueFalseQuestion
        {
            get
            {
                return isTrueFalseQuestion;
            }

            set
            {
                isTrueFalseQuestion = value;
                OnPropertyChanged(nameof(IsTrueFalseQuestion));
            }
        }

        private bool isTextQuestion;

        public bool IsTextQuestion
        {
            get
            {
                return isTextQuestion;
            }

            set
            {
                isTextQuestion = value;
                OnPropertyChanged(nameof(IsTextQuestion));
            }
        }

        private bool isMatchingQuestion;

        public bool IsMatchingQuestion
        {
            get
            {
                return isMatchingQuestion;
            }

            set
            {
                isMatchingQuestion = value;
                OnPropertyChanged(nameof(IsMatchingQuestion));
            }
        }
    }
}
