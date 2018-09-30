using Microsoft.EntityFrameworkCore;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Helpers
{
    public class SportEventsDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }

        public SportEventsDbContext(DbContextOptions<SportEventsDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User Events
            modelBuilder.Entity<UserEvent>()
                .HasKey(ue => new { ue.UserId, ue.EventId});
            modelBuilder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.Users)
                .HasForeignKey(ue => ue.EventId);
            modelBuilder.Entity<UserEvent>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.Events)
                .HasForeignKey(ue => ue.UserId);  

            //User Roles
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId);

            //User Teams
            modelBuilder.Entity<UserTeam>()
                .HasKey(ut => new { ut.UserId, ut.TeamId });
            modelBuilder.Entity<UserTeam>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.Teams)
                .HasForeignKey(ut => ut.UserId);
            modelBuilder.Entity<UserTeam>()
                .HasOne(ut => ut.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(ut => ut.TeamId);
            
        }
    }
}