using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MatchingAnswerDisplay
    { 
        public Guid MatchingAnswerPromptId { get; set; }

        public Guid MatchingAnswerOptionId { get; set; }

        public string PromptText { get; set; }

        public string OptionText { get; set; }


    }
}
