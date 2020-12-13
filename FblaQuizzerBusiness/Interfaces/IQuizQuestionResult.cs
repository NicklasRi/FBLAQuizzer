using System;
using System.Collections.Generic;
using System.Text;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerBusiness.Interfaces
{
    public interface IQuizQuestionResult
    {
        Guid QuizQuestionId { get; set; }

        Guid QuestionId { get; set; }

        string Text { get; set; }

        QuestionType QuestionType { get; set; }

        string Topic { get; set; }

        bool Correct { get; set; }

        int QuestionNumber { get; set; }
    }
}
