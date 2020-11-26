using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FblaQuizzerBusiness;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerWpf.ViewModels
{
    public class QuizzesViewModel
    {
        public IEnumerable<QuizDisplay> Quizzes { get; set; }
    }
}
