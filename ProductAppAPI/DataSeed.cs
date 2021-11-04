using ProductAppAPI.Data;
using ProductAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAppAPI
{
    public static class DataSeed
    {
      
        public static void SeedDB(ProductAppAPIContext context)
        {
            context.Database.EnsureCreated();

            List<Product> products = new()
            {
                new Product
                {
                    Name = "Caneta",
                    Stock = 100,
                    Price = 2.99
                },
                new Product
                {
                    Name = "Lápis",
                    Stock = 200,
                    Price = 0.99
                },
                new Product
                {
                    Name = "Caderno",
                    Stock = 40,
                    Price = 5.99
                },
                new Product
                {
                    Name = "Borracha",
                    Stock = 150,
                    Price = 1.99
                },
                new Product
                {
                    Name = "Lápis de cor",
                    Stock = 50,
                    Price = 12.99
                },
                new Product
                {
                    Name = "Apontador",
                    Stock = 50,
                    Price = 1.99
                },
                new Product
                {
                    Name = "Marca-Texto",
                    Stock = 60,
                    Price = 4.99
                },
            };

            context.AddRange(products);

            context.SaveChanges();
        }
    }
}
