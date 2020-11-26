using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MatchingQuizQuestion: QuizQuestion
    {
        public List<MatchingAnswer> MatchingAnswers { get; set; }
    }
}
