using Microsoft.EntityFrameworkCore;

namespace Chatroom
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Show all of our data from db

            //Entity Framework Core
            //using (var db = new UserDbContext())
            //{
            //    //Ensure that database is created
            //    db.Database.EnsureCreated();

            //    //Query and display users in database
            //    var users = db.Users.ToList();
            //    Console.WriteLine("Users in the database");
            //    foreach (var user in users)
            //    {
            //        Console.WriteLine($"{user.UserId}: {user.UserName}: {user.UserPassword}: {user.Email}:");
            //    }
            //}

            //using (var db = new UserDbContext())
            //{
            //    db.Database.EnsureCreated();

            //    var forgotPassword = new ForgotPassword(db);

            //    forgotPassword.ResetPassword();
            //}
        }
    }


}
