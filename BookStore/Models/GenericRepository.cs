using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System;
using System.Runtime.ConstrainedExecution;
using Dapper;
using static Dapper.SqlMapper;
using BookStore.Models;

namespace WEB_PROJECT.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
    {
        public string connectionString = "Server=(localdb)\\mssqllocaldb;Database=WEB_PROJECT;Trusted_Connection=True;MultipleActiveResultSets=true";
        public void Add(TEntity entity)
        {
            using (var c = new SqlConnection(connectionString))
            {
                var tableName = typeof(TEntity).Name;
                var properties =
              typeof(TEntity).GetProperties().Where(p => p.Name != "Id");

                var columnsNames =
                    string.Join(",", properties.Select(x => x.Name));

                var parameterNames =
                    string.Join(",", properties.Select(x => "@" + x.Name));

                var query = $"insert into {tableName} ({columnsNames}) values ({parameterNames})";
                c.Execute(query, entity);

            }
        }

        public void AddOrder(TEntity entity)
        {
            using (var c = new SqlConnection(connectionString))
            {
                var tableName = typeof(TEntity).Name;
                var properties =
              typeof(TEntity).GetProperties();

                var columnsNames =
                    string.Join(",", properties.Select(x => x.Name));

                var parameterNames =
                    string.Join(",", properties.Select(x => "@" + x.Name));

                var query = $"insert into {tableName} ({columnsNames}) values ({parameterNames})";
                c.Execute(query, entity);

            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var tableName = typeof(TEntity).Name;
                    var properties = typeof(TEntity).GetProperties();

                    string setClause = string.Join(",", properties.Select(p => $"{p.Name}=@{p.Name}"));

                    string query = $"UPDATE {tableName} SET {setClause} WHERE Name=@Name";

                    connection.Execute(query, entity);
                }
            }
            catch (Exception ex)
            {
                // Log or display the error message
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }
        public List<TEntity> GetAll()
        {
            List<TEntity> entityList = new List<TEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var tableName = typeof(TEntity).Name;
                    string query = $"SELECT * FROM {tableName}";
                    return connection.Query<TEntity>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log or display the error message
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return entityList;
        }


        //public void Delete(object name)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            var tableName = typeof(TEntity).Name;
        //            var idPropertyName = "Name"; // Assuming the primary hey property is named "Name"
        //            string query = $"DELETE FROM {tableName} WHERE {idPropertyName} = @Id";

        //            connection.Execute(query, new { Name = name });

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log or display the error messsage
        //        Console.WriteLine("Error occurred: " + ex.Message);
        //    }
        //}


        public void Delete(object id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var tableName = typeof(TEntity).Name;
                    var idPropertyName = "ID"; // Assuming the primary hey property is named "ID"
                    string query = $"DELETE FROM {tableName} WHERE {idPropertyName} = @Id";

                    connection.Execute(query, new { Id = id }); // Corrected parameter name to match query

                }
            }
            catch (Exception ex)
            {
                // Log or display the error message
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }
        public TEntity GetById(object id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var tableName = typeof(TEntity).Name;
                    var idPropertyName = "id";
                    string query = $"SELECT * FROM {tableName} WHERE {idPropertyName} = @Id";

                    return connection.QuerySingleOrDefault<TEntity>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                return default;
            }
        }
        public List<TEntity> Search(object searchString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var tableName = typeof(TEntity).Name;

                var properties = typeof(TEntity).GetProperties()
                                                .Where(p => p.PropertyType == typeof(string)) // Only string properties
                                                .Select(p => p.Name);

                // Build a dynamic query that checks each of these properties
                var conditions = string.Join(" OR ", properties.Select(p => $"{p} LIKE '%' + @SearchString + '%'"));

                var query = $"SELECT * FROM {tableName} WHERE {conditions}";

                return connection.Query<TEntity>(query, new { SearchString = searchString }).ToList();
            }
        }

    }
}
