using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsGoods.Models
{
    public class DataContext : DbContext //class for providing access to database
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts) { } //base class receives configuration information how to connect to server
        public DbSet<Product> Products { get; set; } //for performing CRUD operations

        public DbSet<Category> Categories { get; set; }
    }
}

