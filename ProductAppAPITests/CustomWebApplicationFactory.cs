using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductAppAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAppAPITests
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
           {
               var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ProductAppAPI.Data.ProductAppAPIContext>));
               services.Remove(descriptor);

               
               services.AddDbContext<ProductAppAPI.Data.ProductAppAPIContext>(options =>
               {
                   options.UseInMemoryDatabase("TestingDatabase");
               });

               var sp = services.BuildServiceProvider();

               using (var scope = sp.CreateScope())
               {
                   var scopedServices = scope.ServiceProvider;

           
                   var context = scopedServices.GetRequiredService<ProductAppAPIContext>();
                   if (!context.Product.Any()) ProductAppAPI.DataSeed.SeedDB(context);
                   
                   var db = scopedServices.GetRequiredService<ProductAppAPI.Data.ProductAppAPIContext>();
                   var logger = scopedServices
                       .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                   db.Database.EnsureCreated();

               }
           });

            
        }
    }
}
