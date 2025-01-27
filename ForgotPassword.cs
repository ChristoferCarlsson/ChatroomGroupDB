using Chatroom;
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
                        var user = _db.Users.FirstOrDefault(u => u.Email == userEmail);
                        if (user != null)
                        {
                            // Generera ett nytt enkelt lösenord
                            string newPassword = GenerateSimplePassword();
                            user.UserPassword = newPassword;

                            // Uppdatera databasen
                            _db.SaveChanges();

                            Console.WriteLine($"Password has been reset. New password: {newPassword}");
                        }
                        else
                        {
                            Console.WriteLine("User not found with the provided email.");
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
