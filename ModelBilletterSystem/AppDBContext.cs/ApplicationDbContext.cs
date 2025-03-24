using Microsoft.EntityFrameworkCore;
using ModelBilletterSystem.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Define the relationship between Category and Event
        builder.Entity<Category>()
            .HasOne(c => c.Event)          // Each Category is related to one Event
            .WithMany(e => e.Categories)   // Each Event has many Categories
            .HasForeignKey(c => c.EventId);  // Category has a foreign key to Event

        // Define the relationship between Ticket and Event
        builder.Entity<Ticket>()
            .HasOne(s => s.Events)         // Each Ticket is related to one Event
            .WithMany(g => g.Tickets)     // Each Event has many Tickets
            .HasForeignKey(s => s.EventId);  // Foreign key for Event

        // Set the default value for Create_At
        builder.Entity<Event>()
            .Property(e => e.Create_At)
            .HasDefaultValueSql("GETDATE()"); // Default to the current date and time (SQL Server)
    }
}
