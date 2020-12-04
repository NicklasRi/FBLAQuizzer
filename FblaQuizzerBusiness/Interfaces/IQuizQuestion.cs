using System;
using System.Collections.Generic;
using System.Text;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerBusiness.Interfaces
{
    public interface IQuizQuestion
    {
        Guid Id { get; set; }
        Guid QuizId { get; set; }
        Guid QuestionId { get; set; }
        bool? Correct { get; set; }
        byte QuestionNumber { get; set; }
        QuestionType QuestionType { get; set; }
    }
}
