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
            using (DbConnection cn = Utils.GetConnection())
            {
                cn.Open();

                using (DbCommand command = cn.CreateCommand())
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
                    if (question.Correct == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                    else
                    {
                        parameter.Value = question.Correct;
                    }
                    command.Parameters.Add(parameter);
                    command.ExecuteNonQuery();

                    switch (question.QuestionType)
                    {
                        case QuestionType.MultipleChoice:
                            UpdateMultipleChoiceAnswer(cn, (MultipleChoiceQuizQuestion)question);
                            break;

                        case QuestionType.Text:
                            UpdateTextAnswer(cn, (TextQuizQuestion)question);
                            break;

                        case QuestionType.TrueFalse:
                            UpdateTrueFalseAnswer(cn, (TrueFalseQuizQuestion)question);
                            break;

                        case QuestionType.Matching:
                            UpdateMatchingAnswer(cn, (MatchingQuizQuestion)question);
                            break;
                    }
                }
            } 
        }

        private static void UpdateMultipleChoiceAnswer(DbConnection cn, MultipleChoiceQuizQuestion question)
        {
            using(DbCommand command = cn.CreateCommand())
            {
                command.CommandText = "SELECT QuizQuestionId FROM MultipleChoiceAnswer where QuizQuestionId = @quizQuestionId";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@quizQuestionId";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = question.Id;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@answer";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = question.Answer;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@quizId";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = question.QuizId;
                command.Parameters.Add(parameter);

                if (!command.ExecuteReader().HasRows)
                { 
                    command.CommandText = "INSERT INTO MultipleChoiceAnswer VALUES(@quizQuestionId, @answer, @quizId)";
                }

                else
                {
                    command.CommandText = "UPDATE MultipleChoiceAnswer SET Answer = @answer WHERE QuizQuestionId = @quizQuestionId";                
                }

                command.ExecuteNonQuery();
            }
        }

        private static void UpdateTextAnswer(DbConnection cn, TextQuizQuestion question)
        {
            using (DbCommand command = cn.CreateCommand())
            {
                command.CommandText = "SELECT QuizQuestionId FROM TextAnswer where QuizQuestionId = @quizQuestionId";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@quizQuestionId";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = question.Id;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@answer";
                parameter.DbType = System.Data.DbType.String;
                parameter.Size = 50;
                parameter.Value = question.Answer;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@quizId";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = question.QuizId;
                command.Parameters.Add(parameter);

                if (!command.ExecuteReader().HasRows)
                {
                    command.CommandText = "INSERT INTO TextAnswer VALUES(@quizQuestionId, @answer, @quizId)";
                }

                else
                {
                    command.CommandText = "UPDATE TextAnswer SET Answer = @answer WHERE QuizQuestionId = @quizQuestionId";
                }

                command.ExecuteNonQuery();
            }
        }

        private static void UpdateTrueFalseAnswer(DbConnection cn, TrueFalseQuizQuestion question)
        {
            using (DbCommand command = cn.CreateCommand())
            {
                command.CommandText = "SELECT QuizQuestionId FROM TrueFalseAnswer where QuizQuestionId = @quizQuestionId";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@quizQuestionId";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = question.Id;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@answer";
                parameter.DbType = System.Data.DbType.Boolean;
                parameter.Value = question.Answer;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@quizId";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = question.QuizId;
                command.Parameters.Add(parameter);

                if (!command.ExecuteReader().HasRows)
                {
                    command.CommandText = "INSERT INTO TrueFalseAnswer VALUES(@quizQuestionId, @answer, @quizId)";
                }

                else
                {
                    command.CommandText = "UPDATE TrueFalseAnswer SET Answer = @answer WHERE QuizQuestionId = @quizQuestionId";
                }

                command.ExecuteNonQuery();
            }
        }

        private static void UpdateMatchingAnswer(DbConnection cn, MatchingQuizQuestion question)
        {
            using (DbTransaction transaction = cn.BeginTransaction())
            {
                try
                {
                    using (DbCommand command = cn.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = "SELECT QuizQuestionId FROM MatchingAnswer where QuizQuestionId = @quizQuestionId";

                        DbParameter parameter = command.CreateParameter();
                        parameter.ParameterName = "@quizQuestionId";
                        parameter.DbType = System.Data.DbType.Guid;
                        parameter.Value = question.Id;
                        command.Parameters.Add(parameter);

                        bool hasRows = false;
                        using (DbDataReader reader = command.ExecuteReader()) {
                            hasRows = reader.HasRows;
                        }
                        
                        if(!hasRows)
                        {
                            command.CommandText = "INSERT INTO MatchingAnswer VALUES(@quizQuestionId, @promptId, @optionId, @quizId)";
                            
                            DbParameter promptParameter = command.CreateParameter();
                            promptParameter.ParameterName = "@promptId";
                            promptParameter.DbType = System.Data.DbType.Guid;
                            command.Parameters.Add(promptParameter);

                            DbParameter optionParameter = command.CreateParameter();
                            optionParameter.ParameterName = "@optionId";
                            optionParameter.DbType = System.Data.DbType.Guid;
                            command.Parameters.Add(optionParameter);

                            parameter = command.CreateParameter();
                            parameter.ParameterName = "@quizId";
                            parameter.DbType = System.Data.DbType.Guid;
                            parameter.Value = question.QuizId;
                            command.Parameters.Add(parameter);
                            foreach (MatchingAnswer answer in question.MatchingAnswers)
                            {
                                promptParameter.Value = answer.MatchingAnswerPromptId;
                                optionParameter.Value = answer.MatchingAnswerOptionId;

                                command.ExecuteNonQuery();
                            }

                        }

                        else
                        {
                            command.CommandText = "UPDATE MatchingAnswer SET MatchingAnswerOptionId = @promptId, MatchingAnswerOptionId = @optionId WHERE QuizQuestionId = @quizQuestionId";

                            DbParameter promptParameter = command.CreateParameter();
                            promptParameter.ParameterName = "@promptId";
                            promptParameter.DbType = System.Data.DbType.Guid;
                            command.Parameters.Add(promptParameter);

                            DbParameter optionParameter = command.CreateParameter();
                            optionParameter.ParameterName = "@optionId";
                            optionParameter.DbType = System.Data.DbType.Guid;
                            command.Parameters.Add(optionParameter);

                            parameter = command.CreateParameter();
                            parameter.ParameterName = "@quizId";
                            parameter.DbType = System.Data.DbType.Guid;
                            parameter.Value = question.QuizId;
                            command.Parameters.Add(parameter);

                            foreach (MatchingAnswer answer in question.MatchingAnswers)
                            {
                                promptParameter.Value = answer.MatchingAnswerPromptId;
                                optionParameter.Value = answer.MatchingAnswerOptionId;

                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static IEnumerable<QuizQuestionKey> GetQuizQuestionKeys(Guid quizId)
        {
            List<QuizQuestionKey> questions = new List<QuizQuestionKey>();

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
                    QuizQuestionKey question = new QuizQuestionKey();
                    question.Id = reader.GetGuid(0);
                    question.QuestionId = reader.GetGuid(1);
                    questions.Add(question);
                }
            }

            return questions;
        }

        public static IEnumerable<MatchingAnswerDisplay> GetMatchingAnswersDisplay(Guid quizQuestionId)
        {
            List<MatchingAnswerDisplay> matchingAnswers = new List<MatchingAnswerDisplay>();

            using(DbConnection cn = Utils.GetConnection())
            {
                cn.Open();

                DbCommand command = cn.CreateCommand();
                command.CommandText = @"SELECT MatchingAnswerPromptId, PromptText, MatchingAnswerOptionId, OptionText FROM MatchingAnswer MA 
                JOIN MatchingAnswerPrompt MP ON MA.MatchingAnswerPromptId = MP.Id
                JOIN MatchingAnswerOption MO ON MA.MatchingAnswerOptionId = MO.Id
                WHERE MA.QuizQuestionId = @id";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = quizQuestionId;
                command.Parameters.Add(idParameter);

                using(DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MatchingAnswerDisplay matchingAnswer = new MatchingAnswerDisplay();
                        matchingAnswer.MatchingAnswerPromptId = reader.GetGuid(0);
                        matchingAnswer.PromptText = reader.GetString(1);
                        matchingAnswer.MatchingAnswerOptionId = reader.GetGuid(2);
                        matchingAnswer.OptionText = reader.GetString(3);
                        matchingAnswers.Add(matchingAnswer);

                    }
                }
            }
            return matchingAnswers;
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
                command.CommandText = @"SELECT QQ.Id, QQ.QuizId, QQ.QuestionId, Correct, QuestionNumber, Answer, QuestionType
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
                command.CommandText = @"SELECT QQ.Id, QQ.QuizId, QQ.QuestionId, Correct, QuestionNumber, Answer, QuestionType
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
                command.CommandText = @"SELECT QQ.Id, QQ.QuizId, QQ.QuestionId, Correct, QuestionNumber, Answer, QuestionType
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

                command.CommandText = @"SELECT QuizQuestionId, MatchingAnswerOptionId, MatchingAnswerPromptId 
                FROM MatchingAnswer
                WHERE QuizQuestionId = @id";
                using(DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MatchingAnswer answer = new MatchingAnswer();

                        answer.MatchingAnswerOptionId = reader.GetGuid(1);
                        answer.MatchingAnswerPromptId = reader.GetGuid(2);

                        answers.Add(answer);
                    }
                }
                question.MatchingAnswers = answers;
            }
            return question;
        }
    }
}
