using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System;
using System.Runtime.ConstrainedExecution;

namespace BookStore.Models
{
    public class Productrepository
    {
        string connectionString= "Server=(localdb)\\mssqllocaldb;Database=CheckDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        public void Add(Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Product (Name, Price, Stock, ImageUrl) VALUES (@Name, @Price, @s, @Url)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", product.name);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@s", product.stock);
                        command.Parameters.AddWithValue("@Url", product.ImageUrl);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the error message
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

        public void Update(Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Update Product set Price=@Price,Stock=@s where Name=@Name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", product.name);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@s", product.stock);
                        connection.Open();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the error message
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

        public void Delete(String Name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Delete From Product where Name=@name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", Name);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the error message
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

        public List<Product> GetAll()
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, Price, ImageUrl FROM Product";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                name = reader["Name"].ToString(),
                                Price = (int)reader["Price"],
                                ImageUrl = reader["ImageUrl"].ToString()
                            };
                            productList.Add(product);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            return productList;
        }
    }
}
