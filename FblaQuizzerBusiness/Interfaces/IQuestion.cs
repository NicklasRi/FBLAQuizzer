using System;
using System.Collections.Generic;
using System.Text;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerBusiness.Interfaces
{
    public interface IQuestion
    {
        Guid Id { get; set; }
        string Text { get; set; }
        QuestionType QuestionType { get; set; }
        string Topic { get; set; }
    }
}
