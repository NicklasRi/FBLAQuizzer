using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MultipleChoiceQuestion: Question
    {
        public IEnumerable<MultipleChoiceOption> Options { get; internal set; }
        
        public Guid Answer { get; internal set; }
    }
}
