using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                //Ensure that database is created
                db.Database.EnsureCreated();
                var users = db.Users.ToList();

                try
                {
                    Console.WriteLine("Please enter your username");
                    var username = Console.ReadLine();
                    // Check if the name already exists
                    var existingUserByName = db.Users.FirstOrDefault(u => u.UserName == username);
                    if (existingUserByName == null)
                    {
                        Console.WriteLine("There is no user with this name");
                        return;
                    }

                    Console.WriteLine("Please enter your password");
                    var password = Console.ReadLine();

                    if (existingUserByName.UserPassword != password)
                    {
                        Console.WriteLine("The password is incorrect");
                        return;
                    }

                    Console.WriteLine("You are logged in");
                    loggedin = true;


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
