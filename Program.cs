using Microsoft.EntityFrameworkCore;

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

            //ChatFunction chatFunction = new ChatFunction();
            //chatFunction.Chat();
  
            //Login login = new Login();
            //login.LoginFunktion();

            UserInterface userInterface = new UserInterface();
            userInterface.LoginOrCreateAccountMenu();
        }
    }
}
