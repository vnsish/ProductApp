using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ProductAppAPI.Models;

namespace ProductAppWebApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public EditModel(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }
        [BindProperty]
        public Product product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var response = await _client.GetAsync($"{_configuration["APIurl"]}/api/Products/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return NotFound();
            product = response.Content.ReadFromJsonAsync<Product>().Result;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var content = JsonSerializer.Serialize(product);

            var response = await _client.PutAsync($"{_configuration["APIurl"]}/api/Products/{product.ID}", new StringContent(content, Encoding.UTF8, "application/json"));

            return RedirectToPage("./Index");
        }
    }
}
