using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace FblaQuizzerBusiness.Data
{
    internal static class Utils
    {
        public static DbConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QuizDbConnection"].ConnectionString;

            return new SqlConnection(connectionString);
        }
    }
}
