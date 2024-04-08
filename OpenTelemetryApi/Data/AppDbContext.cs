using Microsoft.EntityFrameworkCore;
using OpenTelemetryApi.Models;
using System.Collections.Generic;

namespace OpenTelemetryApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<LogModel> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
