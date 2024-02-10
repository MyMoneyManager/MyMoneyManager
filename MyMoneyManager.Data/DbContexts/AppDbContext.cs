using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Domain.Entities.AboutUs;

namespace MyMoneyManager.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<AboutUs> AboutUs { get; set; }
    public DbSet<AboutUsAsset> AboutUsAssets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Remainder> Remainders { get; set; }
    public DbSet<Transaction> Tranzactions { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    //}
}
