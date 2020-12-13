using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class TextQuizQuestionResult : QuizQuestionResult
    {
        public string UserAnswer { get; set; }

        public string KeyAnswer { get; set; }
    }
}
