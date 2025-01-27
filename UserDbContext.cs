using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatroom
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=ChatRoomDB;Trusted_Connection=true;TrustServerCertificate=True"); //Connection string to db
        }
    }
}
