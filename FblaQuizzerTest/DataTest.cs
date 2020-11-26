using System;
using FblaQuizzerBusiness;
using FblaQuizzerBusiness.Models;
using FblaQuizzerData.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FblaQuizzerTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void CreateQuizTest()
        {
            Quiz quiz = Quiz.CreateQuiz("TestQuiz");
        }
    }
}
