using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using FblaQuizzerBusiness.Exceptions;
using FblaQuizzerBusiness.Interfaces;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerBusiness.Data
{
    public static class QuizQuestionData
    {
        public static void SaveQuizQuestion(IQuizQuestion question)
        {
            using(DbConnection cn = Utils.GetConnection())
            {
                cn.Open();
                
                using(DbCommand command = cn.CreateCommand())
                {
                    command.CommandText = "UPDATE QuizQuestion SET Correct = @correct where Id = @id";

                    DbParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@id";
                    parameter.DbType = System.Data.DbType.Guid;
                    parameter.Value = question.Id;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@correct";
                    parameter.DbType = System.Data.DbType.Boolean;
                    if(question.Correct == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                    else
                    {
                        parameter.Value = question.Correct;
                    }   
                    command.Parameters.Add(parameter);
                }
            }

            switch (question.QuestionType)
            {
                case QuestionType.MultipleChoice
            }
        }

        public static IEnumerable<QuizQuestionDisplay> GetQuizQuestionsDisplay(Guid quizId)
        {
            List<QuizQuestionDisplay> questions = new List<QuizQuestionDisplay>();

            using (DbConnection cn = Utils.GetConnection())
            {
                cn.Open();
                DbCommand command = cn.CreateCommand();
                command.CommandText = "SELECT Id, QuestionId FROM QuizQuestion WHERE @id = QuizId ORDER BY QuestionNumber";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = quizId;
                command.Parameters.Add(idParameter);

                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    QuizQuestionDisplay question = new QuizQuestionDisplay();
                    question.Id = reader.GetGuid(0);
                    question.QuestionId = reader.GetGuid(1);
                    questions.Add(question);
                }
            }

            return questions;
        }

        public static IQuizQuestion GetQuizQuestion(Guid quizQuestionId)
        {
            using(DbConnection cn = Utils.GetConnection())
            {
                cn.Open();

                using (DbCommand command = cn.CreateCommand())
                {
                    command.CommandText = "SELECT QuestionType FROM QuizQuestion QQ JOIN Question Q ON QQ.QuestionId = Q.Id WHERE QQ.Id = @id";

                    DbParameter idParameter = command.CreateParameter();
                    idParameter.ParameterName = "@id";
                    idParameter.DbType = System.Data.DbType.Guid;
                    idParameter.Value = quizQuestionId;
                    command.Parameters.Add(idParameter);

                    QuestionType questionType = (QuestionType)Convert.ToByte(command.ExecuteScalar());


                    switch (questionType)
                    {
                        case QuestionType.MultipleChoice:
                            return LoadMultipleChoiceQuizQuestion(cn, quizQuestionId);

                        case QuestionType.Text:
                            return LoadTextQuizQuestion(cn, quizQuestionId);

                        case QuestionType.TrueFalse:
                            return LoadTrueFalseQuizQuestion(cn, quizQuestionId);

                        case QuestionType.Matching:
                            return LoadMatchingQuizQuestion(cn, quizQuestionId);

                        default:
                            throw new InvalidQuestionTypeException();

                    }
                }
            }
        }

        private static MultipleChoiceQuizQuestion LoadMultipleChoiceQuizQuestion(DbConnection cn, Guid id)
        {
            MultipleChoiceQuizQuestion question = new MultipleChoiceQuizQuestion();

            using(DbCommand command = cn.CreateCommand())
            {
                command.CommandText = @"SELECT QQ.Id, QQ.QuizId, QQ.QuestionId, Correct, QuestionNumber, Answer 
                FROM QuizQuestion QQ 
                LEFT JOIN MultipleChoiceAnswer MCA ON QQ.Id = MCA.QuizQuestionId
                JOIN Question Q ON QQ.QuestionId = Q.Id 
                WHERE QQ.Id = @id";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using(DbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    question.Id = reader.GetGuid(0);
                    question.QuizId = reader.GetGuid(1);
                    question.QuestionId = reader.GetGuid(2);
                    if (reader.IsDBNull(3))
                    {
                        question.Correct = null;
                    }
                    else
                    {
                        question.Correct = reader.GetBoolean(3);
                    }
                    question.QuestionNumber = reader.GetByte(4);
                    if (reader.IsDBNull(5))
                    {
                        question.Answer = null;
                    }
                    else
                    {
                        question.Answer = reader.GetGuid(5);
                    }
                    question.QuestionType = (QuestionType)reader.GetByte(6);
                }
            }
            return question;
        }

        private static TrueFalseQuizQuestion LoadTrueFalseQuizQuestion(DbConnection cn, Guid id)
        {
            TrueFalseQuizQuestion question = new TrueFalseQuizQuestion();

            using (DbCommand command = cn.CreateCommand())
            {
                command.CommandText = @"SELECT QQ.Id, QQ.QuizId, QQ.QuestionId, Correct, QuestionNumber, Answer 
                FROM QuizQuestion QQ 
                LEFT JOIN MultipleChoiceAnswer MCA ON QQ.Id = MCA.QuizQuestionId 
                JOIN Question Q ON QQ.QuestionId = Q.Id
                WHERE QQ.Id = @id";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    question.Id = reader.GetGuid(0);
                    question.QuizId = reader.GetGuid(1);
                    question.QuestionId = reader.GetGuid(2);
                    if (reader.IsDBNull(3))
                    {
                        question.Correct = null;
                    }
                    else
                    {
                        question.Correct = reader.GetBoolean(3);
                    }
                    question.QuestionNumber = reader.GetByte(4);
                    if (reader.IsDBNull(5))
                    {
                        question.Answer = null;
                    }
                    else
                    {
                        question.Answer = reader.GetBoolean(5);
                    }
                    question.QuestionType = (QuestionType)reader.GetByte(6);
                }
            }
            return question;
        }

        private static TextQuizQuestion LoadTextQuizQuestion(DbConnection cn, Guid id)
        {
            TextQuizQuestion question = new TextQuizQuestion();

            using (DbCommand command = cn.CreateCommand())
            {
                command.CommandText = @"SELECT QQ.Id, QQ.QuizId, QQ.QuestionId, Correct, QuestionNumber, Answer 
                FROM QuizQuestion QQ   
                LEFT JOIN MultipleChoiceAnswer MCA ON QQ.Id = MCA.QuizQuestionId 
                JOIN Question Q ON QQ.QuestionId = Q.Id
                WHERE QQ.Id = @id";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    question.Id = reader.GetGuid(0);
                    question.QuizId = reader.GetGuid(1);
                    question.QuestionId = reader.GetGuid(2);
                    if (reader.IsDBNull(3))
                    {
                        question.Correct = null;
                    }
                    else
                    {
                        question.Correct = reader.GetBoolean(3);
                    }
                    question.QuestionNumber = reader.GetByte(4);
                    if (reader.IsDBNull(5))
                    {
                        question.Answer = null;
                    }
                    else
                    {
                        question.Answer = reader.GetString(5);
                    }
                    question.QuestionType = (QuestionType)reader.GetByte(6);

                }
            }
            return question;
        }

        private static MatchingQuizQuestion LoadMatchingQuizQuestion(DbConnection cn, Guid id)
        {
            MatchingQuizQuestion question = new MatchingQuizQuestion();
            List<MatchingAnswer> answers = new List<MatchingAnswer>();

            using (DbCommand command = cn.CreateCommand())
            {
                command.CommandText = @"SELECT QQ.Id, QQ.QuizId, QuestionId, Correct, QuestionNumber, QuestionType 
                FROM QuizQuestion QQ 
                JOIN Question Q ON QQ.QuestionId = Q.Id 
                WHERE QQ.Id = @id";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    question.Id = reader.GetGuid(0);
                    question.QuizId = reader.GetGuid(1);
                    question.QuestionId = reader.GetGuid(2);
                    if (reader.IsDBNull(3))
                    {
                        question.Correct = null;
                    }
                    else
                    {
                        question.Correct = reader.GetBoolean(3);
                    }
                    question.QuestionNumber = reader.GetByte(4);
                    question.QuestionType = (QuestionType)reader.GetByte(5);
                }

                command.CommandText = @"SELECT Id, QuizQuestionId, MatchingAnswerOptionId, MatchingAnswerPromptId 
                FROM MatchingAnswer
                WHERE QuizQuestionId = @id";
                using(DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MatchingAnswer answer = new MatchingAnswer();

                        answer.Id = reader.GetGuid(0);
                        answer.QuizQuestionId = reader.GetGuid(1);
                        answer.MatchingAnswerOptionId = reader.GetGuid(2);
                        answer.MatchingAnswerPromptId = reader.GetGuid(3);

                        answers.Add(answer);
                    }
                }
                question.MatchingAnswers = answers;
            }
            return question;
        }
    }
}
