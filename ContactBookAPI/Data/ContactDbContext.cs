using ContactBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactBookAPI.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; } 
    }
}
