using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MultipleChoiceQuizQuestion : QuizQuestion
    {
        public Guid? Answer { get; set; }
    }
}
