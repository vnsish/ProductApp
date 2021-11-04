using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductAppAPI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace ProductAppWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public IndexModel(ILogger<IndexModel> logger, HttpClient client, IConfiguration configuration)
        {
            _logger = logger;
            _client = client;
            _configuration = configuration;
        }

        public IList<Product> Products { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public string NameSort { get; set; }
        public string StockSort { get; set; }
        public string PriceSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string SearchString)
        {
            NameSort = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            PriceSort = sortOrder == "price_asc" ? "price_desc" : "price_asc";
            StockSort = sortOrder == "stock_desc" ? "stock_asc" : "stock_desc";

            if (!string.IsNullOrEmpty(SearchString))
            {
                var response = await _client.GetAsync($"{_configuration["APIurl"]}/api/Products/find/{SearchString}");
                response.EnsureSuccessStatusCode();
                Products = response.Content.ReadFromJsonAsync<List<Product>>().Result;
            }
            else 
            { 
                var response = await _client.GetAsync($"{_configuration["APIurl"]}/api/Products");
                response.EnsureSuccessStatusCode();
                Products = response.Content.ReadFromJsonAsync<List<Product>>().Result;
            }

            switch(sortOrder)
            {
                case "name_asc":
                    Products = Products.OrderBy(p => p.Name).ToList();
                    break;
                case "name_desc":
                    Products = Products.OrderByDescending(p => p.Name).ToList();
                    break;
                case "stock_asc":
                    Products = Products.OrderBy(p => p.Stock).ToList();
                    break;
                case "stock_desc":
                    Products = Products.OrderByDescending(p => p.Stock).ToList();
                    break;
                case "price_asc":
                    Products = Products.OrderBy(p => p.Price).ToList();
                    break;
                case "price_desc":
                    Products = Products.OrderByDescending(p => p.Price).ToList();
                    break;
                default:
                    break;
            }
        }

    }
}
