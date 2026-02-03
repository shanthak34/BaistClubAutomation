using BaistClubAutomation.Pages.Models; 
using Microsoft.EntityFrameworkCore;

namespace BaistClubAutomation.Pages.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

      
        public DbSet<ProspectiveMember> ProspectiveMembers { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Member>()
                .Property(m => m.HandicapIndex)
                .HasPrecision(4, 1);
        }
    }
}