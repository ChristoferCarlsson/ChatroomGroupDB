using Microsoft.EntityFrameworkCore;

namespace Chatroom
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserInterface userInterface = new UserInterface();
            userInterface.LoginOrCreateAccountMenu();
        }
    }
}
