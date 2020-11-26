using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MatchingAnswer
    {
        public Guid Id { get; internal set; }
        public Guid MatchingAnswerPromptId { get; internal set; }
        public Guid MatchingAnswerOptionId { get; internal set; }
    }
}
