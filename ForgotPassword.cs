using Chatroom;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public class ForgotPassword
{
    private UserDbContext _db;
    public ForgotPassword(UserDbContext db)
    {
        _db = db;
    }

    public void ResetPassword()
    {
        Console.WriteLine("\nWould you like to reset a password?");

        string[] options = { "Yes", "No" };
        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Would you like to reset a password?");
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex + 1) % options.Length;
                    break;
                case ConsoleKey.Enter:
                    if (options[selectedIndex] == "Yes")
                    {
                        Console.Clear();
                        Console.WriteLine("Enter the email of the user:");
                        string userEmail = Console.ReadLine()?.Trim();

                        // Kontrollera om användaren finns
                        var users = _db.Users
                            .FromSqlRaw("SELECT UserId, UserName, CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('MySecretKey', UserPassword)) AS UserPassword, Email FROM Users")
                            .AsEnumerable()
                            .FirstOrDefault();

                        var user = _db.Users
                            .FromSqlRaw("SELECT UserId, UserName, CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('MySecretKey', UserPassword)) AS UserPassword, Email FROM Users WHERE Email = {0}", userEmail)
                            .AsEnumerable()
                            .FirstOrDefault();

                        if (user != null)
                        {
                            // Generate a new password
                            string newPassword = GenerateSimplePassword();

                            // Convert the new password string to a byte array using UTF8 encoding
                            byte[] newPasswordBytes = System.Text.Encoding.UTF8.GetBytes(newPassword);

                            // Construct the SQL query for updating the user's password
                            string sql = @"
                                UPDATE Users 
                                SET UserPassword = ENCRYPTBYPASSPHRASE(@encryptionKey, @newPassword) 
                                WHERE UserId = @userId";

                            // Execute the SQL query with parameters
                            _db.Database.ExecuteSqlRaw(sql,
                                new SqlParameter("@encryptionKey", "MySecretKey"),  // Make sure you use the correct encryption key
                                new SqlParameter("@newPassword", newPasswordBytes), // Pass the byte array here
                                new SqlParameter("@userId", user.UserId)  // Use the correct UserId of the user you're updating
                            );

                            // Save changes to the database
                            _db.SaveChanges();

                            Console.WriteLine("User's password updated successfully.");
                            Console.WriteLine($"Password has been reset. New password: {newPassword}");
                        }
                        else
                        {
                            Console.WriteLine("No user found with that email.");
                        }

                    }
                    else if (options[selectedIndex] == "No")
                    {
                        Console.Clear();
                        Console.WriteLine("Password reset canceled.");
                    }
                    return; // Avsluta menyn
            }
        }
    }

    // Enkel lösenordsgenerator
    private string GenerateSimplePassword()
    {
        string[] words = { "Hello123", "Summerschool1", "Springbreak55", "Emil1998", "Jakob2001" }; // Lista med enkla ord
        Random random = new Random();

        string chosenWord = words[random.Next(words.Length)]; // Välj ett slumpmässigt ord

        return $"{chosenWord}";
    }
}
