using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class OysterContext : DbContext
    {
        public OysterContext(DbContextOptions<OysterContext> options)
            : base(options)
        {}

        public DbSet<Order> Orders { get; set; }

        public DbSet<SubOrder> SubOrders { get; set; }

        public DbSet<OysterTypePrice> OysterTypePrices { get; set; }
    }
}
