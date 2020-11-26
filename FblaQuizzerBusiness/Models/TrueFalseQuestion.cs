using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class TrueFalseQuestion : Question
    {
        public bool Answer { get; internal set; }
    }
}
