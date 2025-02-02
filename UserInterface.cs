using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    internal class UserInterface
    {
        public void LoginOrCreateAccountMenu() 
        {
           

            string[] options = { "Login", "Forgot password", "Create account", "Exit" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the chatroom");
                Console.WriteLine("-----------------------");

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
                        if (options[selectedIndex] == "Login")
                        {
                            Console.Clear();
                            Login login = new Login();
                            login.LoginFunktion();
                            continue;
                        }
                        else if (options[selectedIndex] == "Forgot password")
                        {
                            //kolla om det fungerar
                            Console.Clear();
                            using (var db = new UserDbContext())
                            {
                                db.Database.EnsureCreated();

                                var forgotPassword = new ForgotPassword(db);

                                forgotPassword.ResetPassword();
                            }
                            continue;
                           
                        }
                        else if (options[selectedIndex] == "Create account")               
                        {
                            Console.Clear();
                            CreateAccount createAccount = new CreateAccount();
                            createAccount.CreateUser();
                            continue;
                        }
                        else if (options[selectedIndex] == "Exit")
                        {
                            Console.Clear();
                        }
                        return; 

                }
            }
        } 
    }
}
