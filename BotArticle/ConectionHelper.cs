using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotArticle
{
    class ConectionHelper
    {
        private static string DataSource = "ADMIN\\SQLEXPRESS";
        private static string UserID = "sa";
        private static string Password = "123@123aa";
        private static string InitialCatalog = "DemoApp";
        private static SqlConnection cnn;
        public static SqlConnection getConection()
        {
            if (cnn == null)
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = DataSource;
                builder.UserID = UserID;
                builder.Password = Password;
                builder.InitialCatalog = InitialCatalog;
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                cnn = connection;
            }
            return cnn;
        }
    }
}
