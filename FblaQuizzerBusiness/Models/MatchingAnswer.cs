using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MatchingAnswer
    { 
        public Guid MatchingAnswerPromptId { get; set; }

        public Guid MatchingAnswerOptionId { get; set; }
    }
}
