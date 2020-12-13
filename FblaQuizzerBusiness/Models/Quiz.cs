using System;
using System.Collections.Generic;
using FblaQuizzerBusiness.Data;
using FblaQuizzerBusiness.Interfaces;
using FblaQuizzerData.Data;

namespace FblaQuizzerBusiness.Models
{
    public class Quiz
    {
        public Quiz() { }

        private Quiz(Guid id, string name, DateTime creationDate)
        {
            this.Id = id;
            this.Name = name;
            this.CreationDate = creationDate;
        }

        public static Quiz CreateQuiz(string name)
        {
            Quiz quiz = new Quiz(Guid.NewGuid(), name, DateTime.Now);
            QuizData.Create(quiz);
            return quiz;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Score { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
