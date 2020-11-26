using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MatchingQuestion : Question
    {
        public IEnumerable<MatchingAnswerPrompt> Prompts { get; internal set; }

        public IEnumerable<MatchingAnswerOption> Options { get; internal set; }

        public IEnumerable<MatchingAnswer> Answers { get; internal set; }

    }
}
