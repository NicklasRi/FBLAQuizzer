using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MatchingQuizQuestion: QuizQuestion
    {
        public IEnumerable<MatchingAnswer> MatchingAnswers { get; set; }
    }
}
