using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using ProductAppAPI.Models;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using Respawn;
using Microsoft.Extensions.Configuration;

namespace ProductAppAPITests
{
    public class IntegrationTests : IClassFixture<CustomWebApplicationFactory<ProductAppAPI.Startup>>
    {
        private HttpClient _client;
        private readonly CustomWebApplicationFactory<ProductAppAPI.Startup> _factory;

        public IntegrationTests(CustomWebApplicationFactory<ProductAppAPI.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

        }

        [Fact]
        public async Task Get_Products()
        {
            var response = await _client.GetAsync("/api/products");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Post_Product()
        {
            Product product = new Product { Name = "Test Product", Stock = 1, Price = 2 };
            var content = JsonSerializer.Serialize(product);
            var response = await _client.PostAsync("/api/products", new StringContent(content, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_Single_Product()
        {
            var response = await _client.GetAsync("/api/products/1");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var product = response.Content.ReadFromJsonAsync<Product>().Result;

            product.Name.Should().Be("Caneta");
        }

        [Fact]
        public async Task Update_Product()
        {
            Product product = new Product { Name = "Test Product", Stock = 1, Price = 2 };
            var content = JsonSerializer.Serialize(product);
            var response = await _client.PostAsync("/api/products", new StringContent(content, Encoding.UTF8, "application/json"));

            product = response.Content.ReadFromJsonAsync<Product>().Result;
            product.Name = "Changed Name";

            content = JsonSerializer.Serialize(product);
            response = await _client.PutAsync($"/api/products/{product.ID}", new StringContent(content, Encoding.UTF8, "application/json"));

            response = await _client.GetAsync($"/api/products/{product.ID}");

            product = response.Content.ReadFromJsonAsync<Product>().Result;
            product.Name.Should().Be("Changed Name");
        }

        [Fact]
        public async Task Delete_Product()
        {
            Product product = new Product { Name = "Test Product", Stock = 1, Price = 2 };
            var content = JsonSerializer.Serialize(product);
            var response = await _client.PostAsync("/api/products", new StringContent(content, Encoding.UTF8, "application/json"));

            product = response.Content.ReadFromJsonAsync<Product>().Result;
            response = await _client.DeleteAsync($"/api/products/{product.ID}");

            response = await _client.GetAsync($"/api/products/{product.ID}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}