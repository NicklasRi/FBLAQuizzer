using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FblaQuizzerBusiness.Interfaces;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerWpf.ViewModels
{
    public class ResultsViewModel : ViewModelBase
    {
        public ResultsViewModel(Quiz quiz, IEnumerable<IQuizQuestionResult> results)
        {
            this.Quiz = quiz;
            this.Results = results;
        }

        private Quiz quiz;

        public Quiz Quiz
        {
            get
            {
                return quiz;
            }

            set
            {
                quiz = value;
                OnPropertyChanged(nameof(Quiz));
            }
        }

        private IEnumerable<IQuizQuestionResult> results;

        public IEnumerable<IQuizQuestionResult> Results
        {
            get
            {
                return results;
            }

            set
            {
                results = value;
                OnPropertyChanged(nameof(Results));
            }
        }
        
    }
}
