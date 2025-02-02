using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    public class CreateAccount
    {
        public void CreateUser()
        {
            // Entity Framework Core
            using (var db = new UserDbContext())
            {
                //Ensure that database is created
                db.Database.EnsureCreated();

                try
                {
                    Console.WriteLine("Please enter your username");
                    var username = Console.ReadLine();
                    // Check if the username already exists
                    var existingUserByName = db.Users
                        .Where(u => u.UserName == username)
                        .Select(u => new { u.UserId, u.UserName, u.Email }) // Only select relevant fields
                        .FirstOrDefault();
                    if (existingUserByName != null)
                    {
                        Console.WriteLine("A user with this username already exists");
                        Console.WriteLine("");
                        Console.WriteLine("Press enter to return to the main menu");
                        Console.ReadLine();
                        return;
                    }

                    Console.WriteLine("Please enter your password");
                    var password = Console.ReadLine();

                    Console.WriteLine("Please enter your email");
                    var email = Console.ReadLine();
                    // Check if the email already exists
                    var existingUserByEmail = db.Users
                        .Where(u => u.Email == email)
                        .Select(u => new { u.UserId, u.UserName, u.Email }) // Only select relevant fields
                        .FirstOrDefault();

                    if (existingUserByEmail != null)
                    {
                        Console.WriteLine("A user with this email already exists.");
                        Console.WriteLine("");
                        Console.WriteLine("Press enter to return to the main menu");
                        Console.ReadLine();
                        return;
                    }

                    string encryptionKey = "MySecretKey";  // Encryption key
                    int nextUserId = (db.Users.Max(u => (int?)u.UserId) ?? 0) + 1;

                    // Convert the password string to a byte array using UTF8 encoding
                    byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

                    // SQL with ENCRYPTBYPASSPHRASE expecting a byte array for encryption
                    string sql = @"
                        INSERT INTO Users (UserId, UserName, UserPassword, Email)
                        VALUES (@p0, @p1, ENCRYPTBYPASSPHRASE(@p2, @p3), @p4)";

                    db.Database.ExecuteSqlRaw(sql, nextUserId, username, encryptionKey, passwordBytes, email);

                    Console.WriteLine("User added successfully with encrypted password.");
                    Console.WriteLine("");
                    Console.WriteLine("Press enter to return to the main menu");
                    Console.ReadLine();
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("");
                    Console.WriteLine("Press enter to return to the main menu");
                    Console.ReadLine();
                }
            }
        }
    }
}
