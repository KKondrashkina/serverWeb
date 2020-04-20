using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Shop
{
    class Shop
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sql1 = "SELECT COUNT(*) FROM Product";

                using (var command = new SqlCommand(sql1, connection))
                {
                    Console.WriteLine("Количество товаров = " + (int)command.ExecuteScalar());
                }

                var sql2 = "INSERT INTO Category(Name) VALUES(N'Meat products')";

                using (var command = new SqlCommand(sql2, connection))
                {
                    command.ExecuteNonQuery();
                }

                var sql3 = "INSERT INTO Product(Name, Price, CategoryId) VALUES(N'Pepperoni', 987, 6)";

                using (var command = new SqlCommand(sql3, connection))
                {
                    command.ExecuteNonQuery();
                }

                var sql4 = "UPDATE Product SET Price = 70 WHERE Name = 'Bananas'";

                using (var command = new SqlCommand(sql4, connection))
                {
                    command.ExecuteNonQuery();
                }

                var sql5 = "DELETE Product WHERE Price < 50";

                using (var command = new SqlCommand(sql5, connection))
                {
                    command.ExecuteNonQuery();
                }

                var sql6 = "SELECT Product.Name, Product.Price, Category.Name FROM Product INNER JOIN Category ON Product.CategoryId=Category.Id";

                using (var command = new SqlCommand(sql6, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t\t{1}\t{2}", reader.GetString(0), reader.GetInt32(1), reader.GetString(2));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                }

                var sql7 = "SELECT Product.Name, Product.Price, Category.Name FROM Product INNER JOIN Category ON Product.CategoryId=Category.Id";

                using (var command = new SqlCommand(sql7, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    foreach (DataTable dataTable in dataSet.Tables)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            var cells = row.ItemArray;

                            foreach (object cell in cells)
                            {
                                Console.Write("{0}\t", cell);
                            }

                            Console.WriteLine();
                        }
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
