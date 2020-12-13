using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MultipleChoiceOption
    {
        public Guid Id { get; set; }

        public string Letter { get; set; }

        public string Text { get; set; }
    }
}
