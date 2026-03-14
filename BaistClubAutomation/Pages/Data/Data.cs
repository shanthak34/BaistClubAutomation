using BaistClubAutomation.Pages.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Add this
using Microsoft.AspNetCore.Identity; // Add this

namespace BaistClubAutomation.Pages.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> // Change here
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipApplication> MembershipApplications { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<MemberAccount> MemberAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Critical for Identity tables

            modelBuilder.Entity<Member>()
                .Property(m => m.HandicapIndex)
                .HasPrecision(4, 1);
        }
    }
}