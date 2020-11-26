using System;
using System.Collections.Generic;
using System.Text;
using FblaQuizzerBusiness.Interfaces;

namespace FblaQuizzerBusiness.Models
{
    public class Question : IQuestion
    {
        public Question(){}

        public Question(Guid id, string text, QuestionType questionType, string topic)
        {
            this.Id = id;
            this.Text = text;
            this.QuestionType = questionType;
            this.Topic = topic; 
        }

        public Guid Id { get; set; }
        public string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Topic { get; set; }
    }
}
