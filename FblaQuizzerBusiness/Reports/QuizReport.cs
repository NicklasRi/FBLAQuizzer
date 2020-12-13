using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FblaQuizzerBusiness.Interfaces;
using FblaQuizzerBusiness.Models;
using FblaQuizzerData.Data;

namespace FblaQuizzerBusiness.Reports
{
    public static class QuizReport
    {
        public static XmlDocument CreateQuizReport(Guid quizId)
        {
            Quiz quiz = QuizData.GetQuiz(quizId);
            IEnumerable<IQuizQuestionResult> results = QuizData.GetResults(quizId);
            XmlMediaTypeFormatter formatter = new XmlMediaTypeFormatter();

            string quizXml = SerializeToXml<Quiz>(formatter, quiz);

            XmlDocument quizDocument = new XmlDocument();
            quizDocument.LoadXml(quizXml);



            foreach(IQuizQuestionResult result in results)
            {
                string resultXml = null;

                switch (result.QuestionType)
                {
                    case QuestionType.MultipleChoice:
                        MultipleChoiceQuizQuestionResult mcResult = (MultipleChoiceQuizQuestionResult)result;
                        resultXml = SerializeToXml<MultipleChoiceQuizQuestionResult>(formatter, mcResult);
                        break;

                    case QuestionType.TrueFalse:
                        TrueFalseQuizQuestionResult tfResult = (TrueFalseQuizQuestionResult)result;
                        resultXml = SerializeToXml<TrueFalseQuizQuestionResult>(formatter, tfResult);
                        break;

                    case QuestionType.Text:
                        TextQuizQuestionResult tResult = (TextQuizQuestionResult)result;
                        resultXml = SerializeToXml<TextQuizQuestionResult>(formatter, tResult);
                        break;

                    case QuestionType.Matching:
                        MatchingQuizQuestionResult mResult = (MatchingQuizQuestionResult)result;
                        resultXml = SerializeToXml<MatchingQuizQuestionResult>(formatter, mResult);
                        break;
                }

                if (resultXml != null)
                {
                    XmlDocument resultsDocument = new XmlDocument();
                    resultsDocument.LoadXml(resultXml);

                    XmlNode resultsNode = quizDocument.ImportNode(resultsDocument.DocumentElement, true);
                    quizDocument.DocumentElement.AppendChild(resultsNode);
                }
            }

            return quizDocument;
        }

        public static string SerializeToXml<T>(XmlMediaTypeFormatter formatter, T value)
        {
            // Create a dummy HTTP Content.
            Stream stream = new MemoryStream();
            var content = new StreamContent(stream);
            /// Serialize the object.
            formatter.WriteToStreamAsync(typeof(T), value, stream, content, null).Wait();
            // Read the serialized string.
            stream.Position = 0;
            return content.ReadAsStringAsync().Result;
        }
    }
}
