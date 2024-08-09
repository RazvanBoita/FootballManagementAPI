using FootballMgm.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballMgm.Api.Data;

public class FootballDbContext : DbContext
{
    public FootballDbContext(DbContextOptions<FootballDbContext> options) : base(options)
    {}

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Footballer> Footballers { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<FootballerRequest> FootballerRequests { get; set; }
    public DbSet<CoachRequest> CoachRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Footballer>()
            .HasOne(f => f.User)
            .WithOne()
            .HasForeignKey<Footballer>(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Coach>()
            .HasOne(c => c.User)
            .WithOne()
            .HasForeignKey<Coach>(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Admin>()
            .HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Admin>(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.Footballers)
            .WithOne(f => f.Team)
            .HasForeignKey(f => f.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Coach)
            .WithOne(c => c.Team)
            .HasForeignKey<Coach>(c => c.TeamId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //TODO 

        modelBuilder.Entity<FootballerRequest>()
            .HasOne(fr => fr.User)
            .WithMany(u => u.FootballerRequests)
            .HasForeignKey(fr => fr.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<CoachRequest>()
            .HasOne(cr => cr.User)
            .WithMany(u => u.CoachRequests)
            .HasForeignKey(cr => cr.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}