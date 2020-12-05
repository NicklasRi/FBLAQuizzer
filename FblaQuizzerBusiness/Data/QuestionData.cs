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
    public static class QuestionData
    {   
        public static IQuestion GetQuestion(Guid id)
        {
            using(DbConnection cn = Utils.GetConnection())
            {
                cn.Open();
                using (DbCommand command = cn.CreateCommand())
                {
                    command.CommandText = "Select QuestionType from Question where @id = id";

                    DbParameter idParameter = command.CreateParameter();
                    idParameter.ParameterName = "@id";
                    idParameter.DbType = System.Data.DbType.Guid;
                    idParameter.Value = id;
                    command.Parameters.Add(idParameter);

                    QuestionType questionType = (QuestionType) Convert.ToByte(command.ExecuteScalar());
                    switch (questionType)
                    {
                        case QuestionType.TrueFalse:
                            return GetTrueFalseQuestion(cn, id);

                        case QuestionType.MultipleChoice:
                            return GetMultipleChoiceQuestion(cn, id);

                        case QuestionType.Text:
                            return GetTextQuestion(cn, id);

                        case QuestionType.Matching:
                            return GetMatchingQuestion(cn, id);

                        default:
                            throw new InvalidQuestionTypeException();
                    }
                }
            }
        }

        public static MultipleChoiceQuestion GetMultipleChoiceQuestion(Guid id)
        {
            MultipleChoiceQuestion question = new MultipleChoiceQuestion();

            string connectionString = ConfigurationManager.ConnectionStrings["QuizDbConnection"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                DbCommand command = cn.CreateCommand();
                command.CommandText = "Select Id, Text, QuestionType, Topic from Question where Id = @id";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    question.Id = reader.GetGuid(0);
                    question.Text = reader.GetString(1);
                    question.QuestionType = (QuestionType)reader.GetByte(2);
                    question.Topic = reader.GetString(3);
                }

                command.CommandText = "Select Id, Text, Letter from MultipleChoiceOption where Id = @questionId";

                List<MultipleChoiceOption> options = new List<MultipleChoiceOption>();

                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MultipleChoiceOption option = new MultipleChoiceOption();
                        option.Id = reader.GetGuid(0);
                        option.Text = reader.GetString(1);
                        option.Letter = reader.GetString(2);
                        options.Add(option);
                    }
                }

                question.Options = options;

                return question;
            }
        }

        public static TrueFalseQuestion GetTrueFalseQuestion(DbConnection cn, Guid id)
        {
            TrueFalseQuestion question = new TrueFalseQuestion();
            using (DbCommand command = cn.CreateCommand())
            {
                command.CommandText = @"Select Q.Id, Text, Topic, QuestionType, Answer from Question Q
                    Join TrueFalseQuestionKey TFQK on Q.Id = TFQK.QuestionId where Q.Id = @id";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    question.Id = reader.GetGuid(0);
                    question.Text = reader.GetString(1);
                    question.Topic = reader.GetString(2);
                    question.QuestionType = (QuestionType)reader.GetByte(3);
                    question.Answer = reader.GetBoolean(4);
                }
                return question;
            }
        }

        public static MultipleChoiceQuestion GetMultipleChoiceQuestion(DbConnection cn, Guid id)
        {
            MultipleChoiceQuestion question = new MultipleChoiceQuestion();
            List<MultipleChoiceOption> options = new List<MultipleChoiceOption>();

            using(DbCommand command = cn.CreateCommand())
            {
                command.CommandText = @"Select Q.Id, Text, Topic, QuestionType, MultipleChoiceOptionId from Question Q
                Join MultipleChoiceQuestionKey MCQK on Q.Id = MCQK.QuestionId where @id = Q.Id";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using(DbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    question.Id = reader.GetGuid(0);
                    question.Text = reader.GetString(1);
                    question.Topic = reader.GetString(2);
                    question.QuestionType = (QuestionType)reader.GetByte(3);
                    question.Answer = reader.GetGuid(4);

                }

                command.CommandText = @"Select Id, Text, Letter from MultipleChoiceOption where @id = QuestionId";
                
                using(DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MultipleChoiceOption option = new MultipleChoiceOption();
                        option.Id = reader.GetGuid(0);
                        option.Text = reader.GetString(1);
                        option.Letter = reader.GetString(2);

                        options.Add(option);
                    }
                }

            }
            question.Options = options;

            return question;
        }

        public static TextQuestion GetTextQuestion(DbConnection cn, Guid id)
        {
            TextQuestion question = new TextQuestion();

            using(DbCommand command = cn.CreateCommand())
            {
                command.CommandText = @"Select Q.Id, Text, Topic, QuestionType, Answer from Question Q
                Join TextQuestionKey TQK on Q.Id = TQK.QuestionId where @id = Q.Id";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using(DbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    question.Id = reader.GetGuid(0);
                    question.Text = reader.GetString(1);
                    question.Topic = reader.GetString(2);
                    question.QuestionType = (QuestionType)reader.GetByte(3);
                    question.Answer = reader.GetString(4);
                }

                return question;
            }
        }

        public static  MatchingQuestion GetMatchingQuestion(DbConnection cn, Guid id)
        {
            MatchingQuestion question = new MatchingQuestion();
            List<MatchingAnswerPrompt> prompts = new List<MatchingAnswerPrompt>();
            List<MatchingAnswerOption> options = new List<MatchingAnswerOption>();
            List<MatchingAnswer> answers = new List<MatchingAnswer>();

            using (DbCommand command = cn.CreateCommand())
            {
                command.CommandText = "Select Id, Text, Topic, QuestionType from Question where @id = Id";

                DbParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    question.Id = reader.GetGuid(0);
                    question.Text = reader.GetString(1);
                    question.Topic = reader.GetString(2);
                    question.QuestionType = (QuestionType)reader.GetByte(3);
                }

                command.CommandText = "Select Id, PromptText from MatchingAnswerPrompt where @id = QuestionId order by [Order]";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MatchingAnswerPrompt prompt = new MatchingAnswerPrompt();
                        prompt.Id = reader.GetGuid(0);
                        prompt.Text = reader.GetString(1);
                        prompts.Add(prompt);
                    }
                }


                command.CommandText = "Select Id, OptionText from MatchingAnswerOption where @id = QuestionId";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MatchingAnswerOption option = new MatchingAnswerOption();
                        option.Id = reader.GetGuid(0);
                        option.Text = reader.GetString(1);
                        options.Add(option);
                    }
                }

                command.CommandText = "Select MatchingAnswerOptionId, MatchingAnswerPromptId from MatchingQuestionKey where @id = QuestionId";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MatchingAnswer answer = new MatchingAnswer();
                        answer.MatchingAnswerOptionId = reader.GetGuid(0);
                        answer.MatchingAnswerPromptId = reader.GetGuid(1);
                        answers.Add(answer);
                    }
                }
            }
            question.Prompts = prompts;
            question.Options = options;
            question.Answers = answers;

            return question;
        }


    }
}
