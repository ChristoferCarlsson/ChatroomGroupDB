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
                var users = db.Users
                    .FromSqlRaw("SELECT UserId, UserName, CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('MySecretKey', UserPassword)) AS UserPassword, Email FROM Users")
                    .AsEnumerable()
                    .FirstOrDefault();

                try
                {
                    Console.WriteLine("Please enter your username");
                    var username = Console.ReadLine();
                    // Check if the name already exists
                    var existingUserByName = db.Users.FirstOrDefault(u => u.UserName == username);
                    if (existingUserByName != null)
                    {
                        Console.WriteLine("A user with this name already exists.");
                        return;
                    }

                    Console.WriteLine("Please enter your password");
                    var password = Console.ReadLine();

                    Console.WriteLine("Please enter your email");
                    var email = Console.ReadLine();
                    // Check if the email already exists
                    var existingUserByEmail = db.Users.FirstOrDefault(u => u.Email == email);
                    if (existingUserByEmail != null)
                    {
                        Console.WriteLine("A user with this email already exists.");
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
                }
                catch (DbUpdateException ex) when (ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE"))
                {
                    Console.WriteLine("A user with this name or email already exists. Please try again.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
