using System;
using System.Collections.Generic;
using System.Text;

namespace FblaQuizzerBusiness.Models
{
    public class MatchingResult
    {
        public string Prompt { get; set; }

        public string UserAnswerOption { get; set; }

        public string KeyAnswerOption { get; set; }
    }
}
