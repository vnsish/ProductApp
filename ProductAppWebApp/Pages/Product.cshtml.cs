using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ProductAppAPI.Models;

namespace ProductAppWebApp.Pages
{
    public class ProductModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public Product product { get; set; }

        public ProductModel(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var response = await _client.GetAsync($"{_configuration["APIurl"]}/api/Products/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return NotFound();
            product = response.Content.ReadFromJsonAsync<Product>().Result;

            return Page();
        }
    }
}
