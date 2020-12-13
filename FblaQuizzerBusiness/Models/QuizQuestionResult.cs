using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using FblaQuizzerBusiness.Interfaces;

namespace FblaQuizzerBusiness.Models
{
    [DataContract]
    public class QuizQuestionResult : IQuizQuestionResult
    {
        public Guid QuizQuestionId { get; set; }

        public Guid QuestionId { get; set; }

        [DataMember(Name = "Text")]
        public string Text { get; set; }

        [DataMember(Name ="QuestionType")]
        public QuestionType QuestionType { get; set; }

        [DataMember(Name ="Topic")]
        public string Topic { get; set; }

        [DataMember(Name ="Correct")]
        public bool Correct { get; set; }
        
        [DataMember(Name ="QuestionNumber")]
        public int QuestionNumber { get; set; }
    }
}
