using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MultipleChoiceQuizQuestionResult : QuizQuestionResult
    {
        public IEnumerable<MultipleChoiceOptionResult> Options { get; set; }

        public MultipleChoiceOptionResult UserAnswer { get; set; }

        public MultipleChoiceOptionResult KeyAnswer { get; set; }
    }
}
