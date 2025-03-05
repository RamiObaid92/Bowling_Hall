using Bowling_Hall.src.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bowling_Hall.src.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
