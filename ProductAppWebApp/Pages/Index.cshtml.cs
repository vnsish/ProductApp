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

        public async Task OnGetAsync()
        {
            var response = await _client.GetAsync($"{_configuration["APIurl"]}/api/Products");
            response.EnsureSuccessStatusCode();
            Products = response.Content.ReadFromJsonAsync<List<Product>>().Result;
        }
    }
}
