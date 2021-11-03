using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductAppAPI.Models;

namespace ProductAppWebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _client;

        [BindProperty]
        public Product product { get; set; }

        public CreateModel(HttpClient client)
        {
            _client = client;
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

            var response = await _client.PostAsync("https://localhost:44387/api/Products", new StringContent(content, Encoding.UTF8, "application/json"));

            return RedirectToPage("./Index");
        }
    }
}
