using DowntimeAlerterWeb.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DowntimeAlerterWeb
{
    public partial class DowntimeAlerterDataContext : DbContext
    {
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<StatusLog> StatusLogs { get; set; }

        public DowntimeAlerterDataContext()
        {
        }

        public DowntimeAlerterDataContext(DbContextOptions<DowntimeAlerterDataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Name=ConnectionStrings:DowntimeAlerterDataContextConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
