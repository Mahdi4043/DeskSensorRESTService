using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DeskSensorRESTService.Models
{
    public class DeskDbContext : DbContext
    {
        public DeskDbContext(DbContextOptions<DeskDbContext> options) : base(options) { }

        public DbSet<Desk> Desk { get; set; }
    }
}
