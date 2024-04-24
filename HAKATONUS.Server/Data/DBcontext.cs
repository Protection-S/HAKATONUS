using HAKATONUS.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace HAKATONUS.Server.Data
{
    public class DBcontext : DbContext
    {

        public DBcontext(DbContextOptions<DBcontext>
                          options) : base(options)
        {
            Database.EnsureCreated();
        }


        public DbSet<Users> User { get; set; }
        public DbSet<ProjectInfo> Event { get; set; }
        public DbSet<Person> Persons{ get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Startup> Startups { get; set; }
        public DbSet<Investors> Investors { get; set; }

    }
}
