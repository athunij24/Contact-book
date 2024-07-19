
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppContacts
{
    internal class ContactsContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
