using Microsoft.EntityFrameworkCore;
using System;

namespace Chatroom
{
    public class Program
    {
        static void Main(string[] args)
        {

            //using (var db = new UserDbContext())
            //{
            //    db.Database.EnsureCreated();

            //    var forgotPassword = new ForgotPassword(db);

            //    forgotPassword.ResetPassword();
            //}

            //CreateAccount createAccount = new CreateAccount();
            //createAccount.CreateUser();

            Login login = new Login();
            login.LoginFunktion();

            //ChatFunction chatFunction = new ChatFunction();
            //chatFunction.Chat();

        }
    }
}
