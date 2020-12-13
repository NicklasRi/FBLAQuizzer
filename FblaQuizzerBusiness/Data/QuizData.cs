using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FblaQuizzerBusiness;
using FblaQuizzerBusiness.Data;
using FblaQuizzerBusiness.Interfaces;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerData.Data
{
    public static class QuizData
    {
        public static void Create(Quiz quiz)
        {            
            List<Guid> questionIds = new List<Guid>();

            using (DbConnection cn = Utils.GetConnection())
            {
                cn.Open();
                string sql = "insert into Quiz values(@id, @name, @score, @creationDate)";
                DbCommand insertCommand = cn.CreateCommand();

                DbParameter idParameter = insertCommand.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = quiz.Id;
                insertCommand.Parameters.Add(idParameter);

                DbParameter nameParameter = insertCommand.CreateParameter();
                nameParameter.ParameterName = "@name";
                nameParameter.DbType = System.Data.DbType.String;
                nameParameter.Size = 50;
                nameParameter.Value = quiz.Name;
                insertCommand.Parameters.Add(nameParameter);

                DbParameter scoreParameter = insertCommand.CreateParameter();
                scoreParameter.ParameterName = "@score";
                scoreParameter.DbType = System.Data.DbType.Decimal;
                scoreParameter.Value = DBNull.Value;
                insertCommand.Parameters.Add(scoreParameter);

                DbParameter creationDateParameter = insertCommand.CreateParameter();
                creationDateParameter.ParameterName = "@creationDate";
                creationDateParameter.DbType = System.Data.DbType.DateTime;
                creationDateParameter.Value = quiz.CreationDate;
                insertCommand.Parameters.Add(creationDateParameter);
                
                insertCommand.CommandText = sql;

                insertCommand.ExecuteNonQuery();

                int questionCount = Convert.ToInt32(ConfigurationManager.AppSettings["QuestionCount"]);
                DbCommand selectQuizIds = cn.CreateCommand();
                selectQuizIds.CommandText = "Select Id from Question";
                using (DbDataReader reader = selectQuizIds.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questionIds.Add(reader.GetGuid(0));
                    }
                }

                int length = questionIds.Count();

                if (length < questionCount) {
                    throw new ApplicationException("Not enough questions in bank to fill quiz");
                }

                int counter = 0;
                List<Guid> selectedQuestionIds = new List<Guid>();
                
                Random random = new Random();

                while (counter < questionCount)
                {
                    int index = random.Next(0, length - 1);
                    Guid questionId = questionIds[index];
                    if (!selectedQuestionIds.Contains(questionId))
                    {
                        selectedQuestionIds.Add(questionId);
                        counter += 1;
                    }
                    
                }

                DbCommand insertQuizQuestionCommand = cn.CreateCommand();
                insertQuizQuestionCommand.CommandText = "Insert into QuizQuestion values(@id, @quizId, @questionId, @correct, @questionNumber)";

                idParameter = insertQuizQuestionCommand.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.DbType = System.Data.DbType.Guid;
                insertQuizQuestionCommand.Parameters.Add(idParameter);

                DbParameter quizIdParameter = insertQuizQuestionCommand.CreateParameter();
                quizIdParameter.ParameterName = "@quizId";
                quizIdParameter.DbType = System.Data.DbType.Guid;
                quizIdParameter.Value = quiz.Id;
                insertQuizQuestionCommand.Parameters.Add(quizIdParameter);

                DbParameter questionIdParameter = insertQuizQuestionCommand.CreateParameter();
                questionIdParameter.ParameterName = "@questionId";
                questionIdParameter.DbType = System.Data.DbType.Guid;
                insertQuizQuestionCommand.Parameters.Add(questionIdParameter);

                DbParameter correctParameter = insertQuizQuestionCommand.CreateParameter();
                correctParameter.ParameterName = "@correct";
                correctParameter.DbType = System.Data.DbType.Boolean;
                correctParameter.Value = DBNull.Value;
                insertQuizQuestionCommand.Parameters.Add(correctParameter);

                DbParameter questionNumberParameter = insertQuizQuestionCommand.CreateParameter();
                questionNumberParameter.ParameterName = "@questionNumber";
                questionNumberParameter.DbType = System.Data.DbType.Byte;
                insertQuizQuestionCommand.Parameters.Add(questionNumberParameter);

                int questionNumber = 1;
                foreach (Guid id in selectedQuestionIds)
                {
                    idParameter.Value = Guid.NewGuid();
                    questionIdParameter.Value = id;
                    questionNumberParameter.Value = questionNumber++;
                    insertQuizQuestionCommand.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<QuizDisplay> GetAll()
        {
            List<QuizDisplay> quizzes = new List<QuizDisplay>();
           
            using (DbConnection cn = Utils.GetConnection())
            {
                cn.Open();
                DbCommand selectCommand = cn.CreateCommand();
                selectCommand.CommandText = "SELECT Id, Name, Score, CreationDate FROM Quiz ORDER BY Name";

                using (DbDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        QuizDisplay quiz = new QuizDisplay();
                        quiz.Id = reader.GetGuid(0);
                        quiz.Name = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                        {
                            quiz.Score = reader.GetDecimal(2);
                        }
                        quiz.CreationDate = reader.GetDateTime(3);
                        quizzes.Add(quiz);
                    }
                }

                return quizzes;

            }
        }

        public static  Quiz GetQuiz(Guid quizId)
        {
            Quiz quiz = new Quiz();

            using(DbConnection cn = Utils.GetConnection())
            {
                cn.Open();
                using(DbCommand command = cn.CreateCommand())
                {
                    command.CommandText = @"SELECT Id, Name, Score, CreationDate 
                    FROM Quiz WHERE Id = @id";

                    DbParameter idParameter = command.CreateParameter();
                    idParameter.ParameterName = "@id";
                    idParameter.DbType = System.Data.DbType.Guid;
                    idParameter.Value = quizId;
                    command.Parameters.Add(idParameter);

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        quiz.Id = reader.GetGuid(0);
                        quiz.Name = reader.GetString(1);
                        if(!reader.IsDBNull(2))
                        {
                            quiz.Score = reader.GetDecimal(2);
                        }
                        quiz.CreationDate = reader.GetDateTime(3);
                    }

                }
            }

            return quiz;
        }

        public static void SaveQuiz(Quiz quiz)
        {
            using(DbConnection cn = Utils.GetConnection())
            {
                cn.Open();

                using(DbCommand command = cn.CreateCommand())
                {
                    command.CommandText = "UPDATE Quiz SET Name=@name, Score=@score WHERE Id=@id";

                    DbParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@id";
                    parameter.DbType = System.Data.DbType.Guid;
                    parameter.Value = quiz.Id;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@name";
                    parameter.DbType = System.Data.DbType.String;
                    parameter.Size = 50;
                    parameter.Value = quiz.Name;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@score";
                    parameter.DbType = System.Data.DbType.Decimal;
                    parameter.Value = quiz.Score;
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<IQuizQuestionResult> GetResults(Guid quizId)
        {
            List<IQuizQuestionResult> results = new List<IQuizQuestionResult>();

            using(DbConnection cn = Utils.GetConnection())
            {
                cn.Open();
                using (DbCommand command = cn.CreateCommand())
                {
                    command.CommandText = @"SELECT Text, QuestionType, Topic, Correct, QuestionNumber, QQ.Id, Q.Id FROM QuizQuestion QQ
                    JOIN Question Q ON QQ.QuestionId = Q.Id 
                    WHERE QQ.QuizId = @id
                    ORDER BY QuestionNumber";

                    DbParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@id";
                    parameter.DbType = System.Data.DbType.Guid;
                    parameter.Value = quizId;
                    command.Parameters.Add(parameter);

                    using(DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuestionType questionType = (QuestionType)reader.GetByte(1);

                            IQuizQuestionResult result = null;

                            switch (questionType)
                            {
                                case QuestionType.MultipleChoice:
                                    result = new MultipleChoiceQuizQuestionResult();
                                    break;

                                case QuestionType.TrueFalse:
                                    result = new TrueFalseQuizQuestionResult();
                                    break;

                                case QuestionType.Text:
                                    result = new TextQuizQuestionResult();
                                    break;

                                case QuestionType.Matching:
                                    result = new MatchingQuizQuestionResult();
                                    break;

                                default:
                                    throw new ApplicationException("Invalid question type");
                            }

                            result.Text = reader.GetString(0);
                            result.QuestionType = questionType;
                            result.Topic = reader.GetString(2);
                            if (reader.IsDBNull(3))
                            {
                                result.Correct = false;
                            }
                            else
                            {
                                result.Correct = reader.GetBoolean(3);
                            }
                            result.QuestionNumber = reader.GetByte(4);
                            result.QuizQuestionId = reader.GetGuid(5);
                            result.QuestionId = reader.GetGuid(6);

                            results.Add(result);
                        }
                    }

                    foreach(IQuizQuestionResult result in results)
                    {
                        switch (result.QuestionType)
                        {
                            case QuestionType.MultipleChoice:
                                LoadMultipleChoiceResult(cn, (MultipleChoiceQuizQuestionResult)result);
                                break;

                            case QuestionType.TrueFalse:
                                LoadTrueFalseResult(cn, (TrueFalseQuizQuestionResult)result);
                                break;

                            case QuestionType.Text:
                                LoadTextResult(cn, (TextQuizQuestionResult)result);
                                break;

                            case QuestionType.Matching:
                                LoadMatchingResult(cn, (MatchingQuizQuestionResult)result);
                                break;
                        }
                    }
                }
            }

            return results;
        }

        private static void LoadMultipleChoiceResult(DbConnection connection, MultipleChoiceQuizQuestionResult quizQuestionResult)
        {
            List<MultipleChoiceOptionResult> optionResults = new List<MultipleChoiceOptionResult>();
            

            using(DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Letter, Text FROM MultipleChoiceOption WHERE QuestionId = @id ORDER BY Letter";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@id";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = quizQuestionResult.QuestionId;
                command.Parameters.Add(parameter);

                using(DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MultipleChoiceOptionResult result = new MultipleChoiceOptionResult();

                        result.Letter = reader.GetString(0);
                        result.Text = reader.GetString(1);
                        optionResults.Add(result);
                    }
                }
            }

            quizQuestionResult.Options = optionResults;

            MultipleChoiceOptionResult optionKey = new MultipleChoiceOptionResult();

            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT Letter, Text FROM MultipleChoiceQuestionKey MCQK 
                JOIN MultipleChoiceOption MCO ON MCQK.QuestionId = MCO.QuestionId 
                WHERE MCQK.QuestionId = @id AND MCQK.MultipleChoiceOptionId = MCO.Id";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@id";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = quizQuestionResult.QuestionId;
                command.Parameters.Add(parameter);

                using(DbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    optionKey.Letter = reader.GetString(0);
                    optionKey.Text = reader.GetString(1);
                }
            }

            quizQuestionResult.KeyAnswer = optionKey;
            MultipleChoiceOptionResult optionAnswer = new MultipleChoiceOptionResult();

            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT Letter, Text FROM MultipleChoiceAnswer MCA 
                LEFT JOIN MultipleChoiceOption MCO ON Answer = MCO.Id
                WHERE MCA.QuizQuestionId = @id";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@id";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = quizQuestionResult.QuizQuestionId;
                command.Parameters.Add(parameter);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (!reader.IsDBNull(0))
                        {
                            optionAnswer.Letter = reader.GetString(0);
                            optionAnswer.Text = reader.GetString(1);
                        }
                    }
                }
            }

            quizQuestionResult.UserAnswer = optionAnswer;
        } 

        private static void LoadTrueFalseResult(DbConnection cn, TrueFalseQuizQuestionResult quizQuestionResult)
        {
            using(DbCommand command = cn.CreateCommand())
            {
                command.CommandText = "SELECT Answer FROM TrueFalseQuestionKey WHERE QuestionId = @id";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@id";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = quizQuestionResult.QuestionId;
                command.Parameters.Add(parameter);

                quizQuestionResult.KeyAnswer = (bool)command.ExecuteScalar();
            }

            using (DbCommand command = cn.CreateCommand())
            {
                command.CommandText = "SELECT Answer FROM TrueFalseAnswer WHERE QuizQuestionId = @id";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@id";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = quizQuestionResult.QuizQuestionId;
                command.Parameters.Add(parameter);

                object userAnswer = command.ExecuteScalar();

                if(userAnswer != null)
                {
                    quizQuestionResult.UserAnswer = (bool)userAnswer;
                }
            }
        }

        private static void LoadTextResult(DbConnection cn, TextQuizQuestionResult quizQuestionResult)
        {
            using (DbCommand command = cn.CreateCommand())
            {
                command.CommandText = "SELECT Answer FROM TextQuestionKey WHERE QuestionId = @id";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@id";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = quizQuestionResult.QuestionId;
                command.Parameters.Add(parameter);

                quizQuestionResult.KeyAnswer = (string)command.ExecuteScalar();
            }

            using (DbCommand command = cn.CreateCommand())
            {
                command.CommandText = "SELECT AnswerText FROM TextAnswer WHERE QuizQuestionId = @id";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@id";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = quizQuestionResult.QuizQuestionId;
                command.Parameters.Add(parameter);

                quizQuestionResult.UserAnswer = (string)command.ExecuteScalar();
            }
        }

        private static void LoadMatchingResult(DbConnection cn, MatchingQuizQuestionResult quizQuestionResult)
        {
            List<MatchingResult> results = new List<MatchingResult>();

            using(DbCommand command = cn.CreateCommand())
            {
                command.CommandText = @"declare @resultTable 
                TABLE(Prompt nvarchar(500), PromptId uniqueidentifier, OptionKey nvarchar(500))
                INSERT INTO @resultTable
                SELECT MAP.PromptText, MAP.Id, MAO.OptionText
                FROM MatchingQuestionKey MQK
                JOIN MatchingAnswerPrompt MAP ON MQK.MatchingAnswerPromptId = MAP.Id
                JOIN MatchingAnswerOption MAO ON MQK.MatchingAnswerOptionId = MAO.Id
                JOIN MatchingAnswer MA ON MQK.MatchingAnswerPromptId = MA.MatchingAnswerPromptId
                WHERE MA.QuizQuestionId = @quizQuestionId

                SELECT Prompt, OptionKey, MAO.OptionText from @resultTable RT
                JOIN MatchingAnswer MA ON RT.promptId = MA.MatchingAnswerPromptId
                AND MA.QuizQuestionId = @quizQuestionId
                JOIN MatchingAnswerOption MAO ON MAO.Id = MA.MatchingAnswerOptionId";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@quizQuestionid";
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = quizQuestionResult.QuizQuestionId;
                command.Parameters.Add(parameter);

                using(DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MatchingResult result = new MatchingResult();

                        result.Prompt = reader.GetString(0);
                        result.KeyAnswerOption = reader.GetString(1);
                        result.UserAnswerOption = reader.GetString(2);

                        results.Add(result);
                    }
                }

            }

            quizQuestionResult.MatchingResults = results;
        }

    }
}
