using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class TrueFalseQuizQuestionResult : QuizQuestionResult
    {
        public bool UserAnswer { get; set; }

        public bool KeyAnswer { get; set; }
    }
}
