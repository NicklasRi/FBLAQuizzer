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

        private int questionIndex;
        public int QuestionIndex
        {
            get
            {
                return questionIndex;
            }

            set
            {
                questionIndex = value;
                OnPropertyChanged(nameof(QuestionIndex));
            }
        }

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

        

        private IEnumerable<MultipleChoiceOption> options;
        public IEnumerable<MultipleChoiceOption> Options
        {
            
            get
            {
                return options;
            }

            set
            {
                options = value;
                OnPropertyChanged("Options");
            }
        }
    }
}
