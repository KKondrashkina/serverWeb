using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Transactions
{
    class Transactions
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    var sql = "INSERT INTO Category(Name) VALUES(N'Household chemicals')";

                    var command = new SqlCommand(sql, connection)
                    {
                        Transaction = transaction
                    };

                    command.ExecuteNonQuery();

                    throw new Exception("Error");
                }
                catch
                {
                    transaction.Rollback();
                }
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql = "INSERT INTO Category(Name) VALUES(N'Goods for pets')";
                var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();

                throw new Exception("Error");
            }
        }
    }
}
