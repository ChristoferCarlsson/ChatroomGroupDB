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
                var users = db.Users.ToList();

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

                    // Add the new user
                    var newUser = new User { UserId = users.Count() + 1, UserName = username, UserPassword = password, Email = email };
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    Console.WriteLine($"User added successfully: {newUser.UserName}, {newUser.Email}");
                    Console.WriteLine("");
                    Console.WriteLine("Press enter to return to the main menu");
                    Console.ReadLine();
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
