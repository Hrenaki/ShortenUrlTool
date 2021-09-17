using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UrlDbContext : DbContext
    {
        public DbSet<UrlEntity> Urls { get; set; }

        public UrlDbContext(DbContextOptions options) : base(options)
        { }
    }
}