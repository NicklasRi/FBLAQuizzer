using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class QuizDisplay
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Score { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
