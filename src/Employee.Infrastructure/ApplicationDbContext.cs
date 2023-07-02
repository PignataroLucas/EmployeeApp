using Employee.Common.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Common.Model.Employee> Employees { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Job> Jobs { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {  
        optionsBuilder.UseSqlite("Filename = Employee.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Address>().HasKey(e => e.Id);
        builder.Entity<Common.Model.Employee>().HasKey(e => e.Id);
        builder.Entity<Team>().HasKey(e => e.Id);
        builder.Entity<Job>().HasKey(e => e.Id);
            
        builder.Entity<Common.Model.Employee>().HasOne(e => e.Address);
        builder.Entity<Common.Model.Employee>().HasOne(e => e.Job);

        builder.Entity<Team>().HasMany(e => e.Employees).WithMany(e => e.Teams);
        
        base.OnModelCreating(builder);
    }
    

}