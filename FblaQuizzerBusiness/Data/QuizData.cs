using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FblaQuizzerBusiness;
using FblaQuizzerBusiness.Models;

namespace FblaQuizzerData.Data
{
    public static class QuizData
    {
        public static void Create(Quiz quiz)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuizDbConnection"].ConnectionString;
            
            List<Guid> questionIds = new List<Guid>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                string sql = "insert into Quiz values(@id, @name)";
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
            string connectionString = ConfigurationManager.ConnectionStrings["QuizDbConnection"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                DbCommand selectCommand = cn.CreateCommand();
                string sql = "select id,name from Quiz order by name";
                selectCommand.CommandText = sql;

                DbDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    QuizDisplay quiz = new QuizDisplay();
                    quiz.Id = reader.GetGuid(0);
                    quiz.Name = reader.GetString(1);
                    quizzes.Add(quiz);
                }

                return quizzes;

            }
        }

        
    }
}
