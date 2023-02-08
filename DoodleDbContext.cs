
using Microsoft.EntityFrameworkCore;

namespace Doodle
{
    internal class DoodleDbContext : DbContext
    {
        public DoodleDbContext(DbContextOptions<DoodleDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message =>
            {
                Console.WriteLine(message);
            }, Microsoft.Extensions.Logging.LogLevel.Trace);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>()
                .HasKey(q => q.QuoteId);

            // modelBuilder.Entity<Account>()
            //     .HasKey(a => a.AccountId);

            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Account)
                .WithOne()
                .HasForeignKey<Quote>(q => q.CustomerId);
        }

        //public DbSet<Account> Accounts { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}