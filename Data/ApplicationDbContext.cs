using Game_Universe.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Game_Universe.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }



        public DbSet<Game> Game { get; set; }


    }
}
