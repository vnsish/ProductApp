using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductAppAPI.Models;

namespace ProductAppWebApp.Pages
{
    public class ProductModel : PageModel
    {
        private readonly HttpClient _client;

        public Product product { get; set; }

        public ProductModel(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var response = await _client.GetAsync($"https://localhost:44387/api/Products/{id}");
            response.EnsureSuccessStatusCode();
            product = response.Content.ReadFromJsonAsync<Product>().Result;

            return Page();
        }
    }
}
