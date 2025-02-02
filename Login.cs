using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Chatroom
{
    public class Login
    {
        public void LoginFunktion()
        {
            bool loggedin = false;
            // Entity Framework Core
            using (var db = new UserDbContext())
            {
                // Ensure that the database is created
                db.Database.EnsureCreated();

                try
                {
                    Console.WriteLine("Please enter your username");
                    var username = Console.ReadLine();


                    // Check if the user exists and directly decrypt the password in the query
                    var existingUserByName = db.Users
                        .FromSqlRaw("SELECT UserId, UserName, CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('MySecretKey', UserPassword)) AS UserPassword, Email FROM Users WHERE UserName = {0}", username)
                        .AsEnumerable()
                        .FirstOrDefault();

                    if (existingUserByName == null)
                    {
                        Console.WriteLine("There is no user with this name");
                        Console.WriteLine("");
                        Console.WriteLine("Press enter to return to the main menu");
                        Console.ReadLine();
                        return;
                    }

                    Console.WriteLine("Please enter your password");
                    var password = Console.ReadLine();

                    // The decrypted password is now a string in existingUserByName.UserPassword
                    if (existingUserByName.UserPassword != password)
                    {
                        Console.WriteLine("The password is incorrect");
                        Console.WriteLine("");
                        Console.WriteLine("Press enter to return to the main menu");
                        Console.ReadLine();
                        return;
                    }

                    Console.WriteLine("You are logged in");
                    loggedin = true;

                    ChatFunction chatFunction = new ChatFunction();
                    chatFunction.Chat(existingUserByName.UserId);

                }
                
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("");
                    Console.WriteLine("Press enter to return to the main menu");
                    Console.ReadLine();
                }
                return;
            }
        }
    }
}
