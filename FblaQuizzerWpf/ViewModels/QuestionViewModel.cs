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
        private bool isFirstQuestion = true;
        private bool isLastQuestion = false;
        public QuestionViewModel(Quiz quiz)
        {
            this.Quiz = quiz;
        }

        public Quiz Quiz { get; }

        public int QuestionIndex { get; set; }

        public IQuestion Question { get; set; }

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

        public IEnumerable<MultipleChoiceOption> Options
        {
            get
            {
                MultipleChoiceQuestion question = this.Question as MultipleChoiceQuestion;
                return question == null ? null : question.Options;
            }
        }
    }
}
