using Microsoft.EntityFrameworkCore;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Core.Models.Events;

namespace SystemSupportingMSE.Helpers
{
    public class SportEventsDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }

        public SportEventsDbContext(DbContextOptions<SportEventsDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //EventsCompetitions
            modelBuilder.Entity<EventCompetition>()
                .HasKey(ec => new { ec.CompetitionId, ec.EventId});
            modelBuilder.Entity<EventCompetition>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.Competitions)
                .HasForeignKey(ec => ec.EventId);
            modelBuilder.Entity<EventCompetition>()
                .HasOne(ec => ec.Competition)
                .WithMany(c => c.Events)
                .HasForeignKey(ec => ec.CompetitionId);  

            //UsersRoles
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

            //UsersTeams
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