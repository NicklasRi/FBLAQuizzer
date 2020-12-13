using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FblaQuizzerBusiness.Models
{
    public class MatchingQuizQuestion: QuizQuestion
    {
        public IEnumerable<MatchingAnswer> MatchingAnswers { get; set; }

        public void Grade(MatchingQuestion question)
        {
            bool correct = true;

            foreach (MatchingAnswer answerKey in question.Answers)
            {
                Guid promptId = answerKey.MatchingAnswerPromptId;
                MatchingAnswer answer = this.MatchingAnswers.First(p => p.MatchingAnswerPromptId == promptId);

                if (answerKey.MatchingAnswerOptionId != answer.MatchingAnswerOptionId)
                {
                    correct = false;
                    break;
                }
            }

            this.Correct = correct;
        }
    }
}
