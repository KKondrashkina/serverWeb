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

                var productCountSql = "SELECT COUNT(*) FROM Product";

                using (var command = new SqlCommand(productCountSql, connection))
                {
                    Console.WriteLine("Количество товаров = " + (int)command.ExecuteScalar());
                }

                var newCategorySql = "INSERT INTO Category(Name) " +
                           "VALUES(@name)";

                using (var command = new SqlCommand(newCategorySql, connection))
                {
                    var name = "Meat products";

                    command.Parameters.Add(new SqlParameter("@name", name)
                    {
                        SqlDbType = SqlDbType.NVarChar
                    });

                    command.ExecuteNonQuery();
                }

                var newProductSql = "INSERT INTO Product(Name, Price, CategoryId) " +
                           "VALUES(@name, @price, @categoryId)";

                using (var command = new SqlCommand(newProductSql, connection))
                {
                    var name = "Pepperoni";
                    var price = 987;
                    var categoryId = 6;

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@name", name)
                        {
                            SqlDbType = SqlDbType.NVarChar
                        },
                        new SqlParameter("@price", price)
                        {
                            SqlDbType = SqlDbType.Int
                        },
                        new SqlParameter("@categoryId", categoryId)
                        {
                            SqlDbType = SqlDbType.Int
                        }
                    });

                    command.ExecuteNonQuery();
                }

                var newPriceSql = "UPDATE Product " +
                           "SET Price = @price " +
                           "WHERE Name = @name";

                using (var command = new SqlCommand(newPriceSql, connection))
                {
                    var name = "Bananas";
                    var price = 70;

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@name", name)
                        {
                            SqlDbType = SqlDbType.NVarChar
                        },
                        new SqlParameter("@price", price)
                        {
                            SqlDbType = SqlDbType.Int
                        }
                    });

                    command.ExecuteNonQuery();
                }

                var productRemovingSql = "DELETE Product " +
                           "WHERE Price < @price";

                using (var command = new SqlCommand(productRemovingSql, connection))
                {
                    var price = 50;

                    command.Parameters.Add(new SqlParameter("@price", price)
                    {
                        SqlDbType = SqlDbType.Int
                    });

                    command.ExecuteNonQuery();
                }

                var allProductsWithCategorySql = "SELECT Product.Name, Product.Price, Category.Name " +
                           "FROM Product " +
                           "INNER JOIN Category " +
                           "ON Product.CategoryId = Category.Id";

                using (var command = new SqlCommand(allProductsWithCategorySql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t\t{1}\t{2}", reader.GetString(0), reader.GetInt32(1),
                                    reader.GetString(2));
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                    }
                }

                using (var command = new SqlCommand(allProductsWithCategorySql, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataSet = new DataSet();

                        adapter.Fill(dataSet);

                        foreach (DataTable dataTable in dataSet.Tables)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                var cells = row.ItemArray;

                                foreach (var cell in cells)
                                {
                                    Console.Write("{0}\t", cell);
                                }

                                Console.WriteLine();
                            }
                        }

                    }
                }
            }

            Console.ReadKey();
        }
    }
}