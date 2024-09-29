using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Строка підключення до SQL Server
        string connectionString = "Server=DESKTOP-B3U7H54\\SQLEXPRESS01;Database=PostServiceDB;Trusted_Connection=True;";

        // Відкриття з'єднання з базою даних
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Виконання запиту на вставку нових даних
            string insertQuery = "INSERT INTO Customers (FirstName, LastName, Address, Phone) VALUES (@FirstName, @LastName, @Address, @Phone)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@FirstName", "Olena");
                insertCommand.Parameters.AddWithValue("@LastName", "Shevchenko");
                insertCommand.Parameters.AddWithValue("@Address", "Odesa, Ukraine");
                insertCommand.Parameters.AddWithValue("@Phone", "380503456789");

                // Виконання команди на вставку даних
                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Record successfully added!");
            }

            // Виконання запиту на вибірку даних (SELECT)
            SqlCommand command = new SqlCommand("SELECT * FROM Customers", connection);
            SqlDataReader reader = command.ExecuteReader();

            // Виведення даних на екран
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["CustomerID"]}, Name: {reader["FirstName"]} {reader["LastName"]}, Phone: {reader["Phone"]}");
            }

            reader.Close();
        }
    }
}
