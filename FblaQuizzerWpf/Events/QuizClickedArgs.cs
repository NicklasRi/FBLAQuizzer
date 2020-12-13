using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FblaQuizzerWpf.Events
{
    public class QuizClickedArgs : EventArgs
    {
        public QuizClickedArgs(Guid quizId)
        {
            this.QuizId = quizId;
        }

        public Guid QuizId { get; set; }
    }
}
