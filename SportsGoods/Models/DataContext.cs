using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsGoods.Models
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts) { } //base class receives configuration information how to connect to server
        public DbSet<Product> Products { get; set; } //for performing CRUD operations
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> orderLines{ get; set; }

    }
}

