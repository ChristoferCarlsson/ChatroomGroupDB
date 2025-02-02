using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    public class ChatFunction
    {
        bool chatting = false;
        public void Chat(int userId)
        {
            //We let the user know that the messages are loading 
            Console.Write("Loading posts...");

            chatting = true;
            ReadPosts();
            Console.WriteLine("To stop chatting please write ''log me out''");
            Console.WriteLine($"");

            //We create a loop
            while (chatting)
            {
                {
                    chatting = false;
                    break;
                };
            }
        }

        public void ReadPosts()
        {
            using (var context = new UserDbContext())
            {
                //We combine posts with users so that we can see the username 
                var posts = context.Posts
                   .Include(p => p.User)
                   .Select(p => new
                   {
                       p.PostId,
                       p.Text,
                       UserName = p.User.UserName
                   })
                   .ToList();

                Console.Clear();
                foreach (var post in posts)
                {
                    Console.WriteLine($"{post.UserName}");
                    Console.WriteLine($"{post.Text}");
                    Console.WriteLine($"");
                }
            }
        }

        public bool Post(int userId)
        {
            using (var db = new UserDbContext())
            {
                //Ensure that database is created
                db.Database.EnsureCreated();
                var posts = db.Posts.ToList();

                try
                {

                    var message = Console.ReadLine();

                    //We make sure that the user can log out
                    if (message == "log me out") return false;

                    //Clear the written line
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();

                    // Add the new post
                    var newPost = new Post { PostId = posts.Count() + 1, Text = message, UserId = userId };
                    db.Posts.Add(newPost);
                    db.SaveChanges();

                    Console.WriteLine($"{"You"}");
                    Console.WriteLine($"{message}");
                    Console.WriteLine($"");

                }

                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                return false;
            }
        }
        //We clear the written message
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
