using System;
using System.Collections.Generic;
using System.Text;
using FblaQuizzerBusiness.Interfaces;

namespace FblaQuizzerBusiness.Models
{
    public class QuizQuestion: IQuizQuestion
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Topic { get; set; }
        public bool? Correct { get; set; }
        public byte QuestionNumber { get; set; }
    }
}
