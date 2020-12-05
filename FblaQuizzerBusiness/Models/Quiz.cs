﻿using System;
using System.Collections.Generic;
using FblaQuizzerBusiness.Data;
using FblaQuizzerBusiness.Interfaces;
using FblaQuizzerData.Data;

namespace FblaQuizzerBusiness.Models
{
    public class Quiz
    {
        private Quiz() { }

        private Quiz(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public static Quiz CreateQuiz(string name)
        {
            Quiz quiz = new Quiz(Guid.NewGuid(), name);
            QuizData.Create(quiz);
            return quiz;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
