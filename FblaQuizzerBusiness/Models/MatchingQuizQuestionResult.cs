using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MatchingQuizQuestionResult : QuizQuestionResult
    {
        public IEnumerable<MatchingResult> MatchingResults { get; set; }
    }
}
