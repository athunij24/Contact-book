
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ConsoleAppContacts
{
    internal class ContactsContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
