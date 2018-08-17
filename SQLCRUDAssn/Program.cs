using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SQLCRUDAssn
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.debug.json")
#else
                .AddJsonFile("appsettings.release.json")
#endif
                .Build();

            string connStr = configBuilder.GetConnectionString("DefaultConnection");



            Console.WriteLine("What category do you want to add?");
            string newCategory = Console.ReadLine();
            

            Console.WriteLine("What is the most you want to pay for a product today?");
            int maxPrice = int.Parse(Console.ReadLine());
            List<string> values  = GetProductNamesInPriceRange(maxPrice);

            foreach (string value in values)
            {
                Console.WriteLine(value);
            }
            Console.ReadLine();

            Console.WriteLine("What do you want the price of the Samsung Galaxy to be?");
            int newPrice = int.Parse(Console.ReadLine());
            

            Console.WriteLine("What product do you want to delete?");
            string productToDelete = Console.ReadLine();
        }

        static void AddNewCategory(string newCategory)
        {
            
            IConfiguration configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.debug.json")
#else
                .AddJsonFile("appsettings.release.json")
#endif
                .Build();

            string connStr = configBuilder.GetConnectionString("DefaultConnection");

            MySqlConnection conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO categories (Name) VALUES (@newCategory)";
                cmd.Parameters.AddWithValue("newCategory", newCategory);

                cmd.ExecuteNonQuery();
            }

        }

        static List<string> GetProductNamesInPriceRange(int maxPrice)
        {
            
            IConfiguration configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.debug.json")
#else
                .AddJsonFile("appsettings.release.json")
#endif
                .Build();

            string connStr = configBuilder.GetConnectionString("DefaultConnection");

            MySqlConnection conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Name FROM products WHERE Price <= @maxPrice;";
                cmd.Parameters.AddWithValue("maxPrice", maxPrice);

                MySqlDataReader dr = cmd.ExecuteReader();

                List<string> productNames = new List<string>();

                while (dr.Read())
                {
                    productNames.Add(dr["Name"].ToString());
                }

                return productNames;
            }
        }

        static void PriceChange(int newPrice)
        {
            
            IConfiguration configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.debug.json")
#else
                .AddJsonFile("appsettings.release.json")
#endif
                .Build();

            string connStr = configBuilder.GetConnectionString("DefaultConnection");

            MySqlConnection conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Samsung Galaxy FROM products SET Price = @newPrice;";
                cmd.Parameters.AddWithValue("newPrice", newPrice);

                cmd.ExecuteNonQuery();
            }
        }

        static void DeleteProduct(string productToDelete)
        {
            
            IConfiguration configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.debug.json")
#else
                .AddJsonFile("appsettings.release.json")
#endif
                .Build();

            string connStr = configBuilder.GetConnectionString("DefaultConnection");

            MySqlConnection conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE Name LIKE '%@productToDelete%";
                cmd.Parameters.AddWithValue("productToDelete", productToDelete);

                cmd.ExecuteNonQuery();
            }

        }
    }
}
