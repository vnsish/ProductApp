using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductAppAPI.Models;

namespace ProductAppAPI.Data
{
    public class ProductAppAPIContext : DbContext
    {
        public ProductAppAPIContext (DbContextOptions<ProductAppAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ProductAppAPI.Models.Product> Product { get; set; }
    }
}
