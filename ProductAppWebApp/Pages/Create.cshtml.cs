using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ProductAppAPI.Models;

namespace ProductAppWebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public Product product { get; set; }

        public CreateModel(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var content = JsonSerializer.Serialize(product);

            var response = await _client.PostAsync($"{_configuration["APIurl"]}/api/Products", new StringContent(content, Encoding.UTF8, "application/json"));

            return RedirectToPage("./Index");
        }
    }
}
