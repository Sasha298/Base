using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using lab_2_1.Models;
using lab_2_1.Data;

namespace lab_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("Server=DESKTOP-B3U7H54\\SQLEXPRESS01;Database=ComputerPartsShop;Trusted_Connection=True;TrustServerCertificate=True;");
            conn.Open();

            Console.WriteLine("Select an option from the list below");
            Console.WriteLine("0 - Get all tables");
            Console.WriteLine("1 - Select Table");
            Console.WriteLine("2 - Insert Value to Table [Product]");
            Console.WriteLine("3 - Join Tables [OrderDetails]");
            Console.WriteLine("4 - Filter Products by Price");
            Console.WriteLine("5 - Get Aggregate Data [Total Orders]");

            int swt = Convert.ToInt32(Console.ReadLine());
            switch (swt)
            {
                case 0:
                    {
                        Selects sel = new Selects(conn);
                        sel.GetTables();
                        break;
                    }
                case 1:
                    {
                        Selects sel = new Selects(conn);
                        Console.WriteLine("Enter table name:");
                        string tbln = Console.ReadLine();
                        sel.SelectAllItems(tbln);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter product name, price, categoryID, manufacturerID:");
                        string[] values = Console.ReadLine().Split(',');

                        var product = new Product
                        {
                            ProductName = values[0],
                            Price = Convert.ToDecimal(values[1]),
                            CategoryID = Convert.ToInt32(values[2]),
                            ManufacturerID = Convert.ToInt32(values[3])
                        };

                        using (var context = new AppDbContext())
                        {
                            context.Products.Add(product);
                            try
                            {
                                context.SaveChanges();
                                Console.WriteLine("Product added successfully.");
                            }
                            catch (DbUpdateException ex)
                            {
                                Console.WriteLine($"An error occurred: {ex.InnerException?.Message}");
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        Selects sel = new Selects(conn);
                        sel.JoinOrderDetails();
                        break;
                    }
                case 4:
                    {
                        Selects sel = new Selects(conn);
                        Console.WriteLine("Enter maximum price:");
                        decimal price = Convert.ToDecimal(Console.ReadLine());
                        sel.FilterProductsByPrice(price);
                        break;
                    }
                case 5:
                    {
                        Selects sel = new Selects(conn);
                        sel.GetTotalOrders();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Close Program");
                        break;
                    }
            }

            conn.Close();
        }
    }

    class Selects
    {
        private SqlConnection _conn;

        public Selects(SqlConnection conn)
        {
            _conn = conn;
        }

        public void GetTables()
        {
            SqlCommand cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", _conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["TABLE_NAME"]);
            }
            reader.Close();
        }

        public void SelectAllItems(string tableName)
        {
            SqlCommand cmd = new SqlCommand($"SELECT * FROM {tableName}", _conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + "\t");
                }
                Console.WriteLine();
            }
            reader.Close();
        }

        public void JoinOrderDetails()
        {
            string query = "SELECT o.OrderID, c.CustomerName, p.ProductName, od.Quantity FROM [Order] o " +
                           "JOIN Customer c ON o.CustomerID = c.CustomerID " +
                           "JOIN OrderDetails od ON o.OrderID = od.OrderID " +
                           "JOIN Product p ON od.ProductID = p.ProductID";
            SqlCommand cmd = new SqlCommand(query, _conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"OrderID: {reader["OrderID"]}, Customer: {reader["CustomerName"]}, Product: {reader["ProductName"]}, Quantity: {reader["Quantity"]}");
            }
            reader.Close();
        }

        public void FilterProductsByPrice(decimal price)
        {
            string query = "SELECT ProductName, Price FROM Product WHERE Price <= @Price";
            SqlCommand cmd = new SqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("@Price", price);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Product: {reader["ProductName"]}, Price: {reader["Price"]}");
            }
            reader.Close();
        }

        public void GetTotalOrders()
        {
            string query = "SELECT COUNT(*) AS TotalOrders FROM [Order]";
            SqlCommand cmd = new SqlCommand(query, _conn);
            int totalOrders = (int)cmd.ExecuteScalar();
            Console.WriteLine($"Total Orders: {totalOrders}");
        }
    }

    class Insert
    {
        private SqlConnection _conn;

        public Insert(SqlConnection conn)
        {
            _conn = conn;
        }

        public void InsertProduct(string productName, decimal price, int categoryID, int manufacturerID)
        {
            string query = "INSERT INTO Product (ProductName, Price, CategoryID, ManufacturerID) VALUES (@ProductName, @Price, @CategoryID, @ManufacturerID)";
            SqlCommand cmd = new SqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("@ProductName", productName);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@CategoryID", categoryID);
            cmd.Parameters.AddWithValue("@ManufacturerID", manufacturerID);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product added successfully.");
        }
    }
}
namespace lab_2_1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-B3U7H54\\SQLEXPRESS01;Database=ComputerPartsShop;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturerID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID);
        }
    }
}