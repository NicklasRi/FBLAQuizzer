using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FblaQuizzerBusiness.Interfaces;

namespace FblaQuizzerWpf.Controls
{
    public class QuestionControl : UserControl
    {
        public QuestionControl() { }

        public static readonly DependencyProperty QuizQuestionProperty = DependencyProperty
            .Register("QuizQuestion", typeof(IQuizQuestion),
            typeof(QuestionControl), new PropertyMetadata(null));

        public IQuizQuestion QuizQuestion
        {
            get
            {
                return (IQuizQuestion)this.GetValue(QuizQuestionProperty);
            }

            set
            {
                this.SetValue(QuizQuestionProperty, value);
            }
        }

    }
}
