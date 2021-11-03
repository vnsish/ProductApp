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

namespace ProductAppWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _client;

        public IndexModel(ILogger<IndexModel> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        public IList<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            var response = await _client.GetAsync("https://localhost:44387/api/Products");
            response.EnsureSuccessStatusCode();
            Products = response.Content.ReadFromJsonAsync<List<Product>>().Result;
        }
    }
}
