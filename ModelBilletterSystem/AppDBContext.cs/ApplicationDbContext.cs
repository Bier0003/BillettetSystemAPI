using Microsoft.EntityFrameworkCore;
using ModelBilletterSystem;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Ticket>()
            .HasOne(s => s.Event);
        //.WithMany(g => g.Tickets)
        //.HasForeignKey(s => s.EventId);

        builder.Entity<Event>()
            .HasOne(c => c.Category);
            //.HasForeignKey(c => c.CategoryId);

        builder.Entity<Event>()
            .Property(e => e.Create_At)
            .HasDefaultValueSql("GETDATE()"); // Fix: Add using directive for Microsoft.EntityFrameworkCore.Metadata.Builders
    }
}
