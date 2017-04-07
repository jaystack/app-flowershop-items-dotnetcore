using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using App.Flowershop.Items.Models;
using App.Flowershop.Items.ViewModels;
using Microsoft.Extensions.Options;
using SystemEndpointsDotnetCore;

namespace App.Flowershop.Items.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<Config> config;
        private IStore store;

        public HomeController(IOptions<Config> optionsAccessor)
        {
            config = optionsAccessor;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            store = new Store(config.Value.hosts);

            var data = await getResponseAsync("http://" + store.GetServiceAddress(config.Value.DataApi), "data/categories");

            var vm = new CategoriesViewModel();

            vm.Categories = JsonConvert.DeserializeObject<List<Category>>(data);

            return PartialView("Index", vm);
        }

        [HttpGet("/checkout")]
        public async Task<IActionResult> Checkout()
        {
            store = new Store(config.Value.hosts);
            var categoires = await getResponseAsync("http://" + store.GetServiceAddress(config.Value.DataApi), "data/categories");
            var flowers = await getResponseAsync("http://" + store.GetServiceAddress(config.Value.DataApi), "data/flowers/");

            var vm = new CategoriesViewModel();
            vm.Categories = JsonConvert.DeserializeObject<List<Category>>(categoires);
            vm.FlowerList = JsonConvert.DeserializeObject<List<Flower>>(flowers);

            return PartialView("Index", vm);
        }

        [HttpGet("/category/{catName}")]
        public async Task<IActionResult> Category(string catName)
        {
            store = new Store(config.Value.hosts);
            var categoires = await getResponseAsync("http://" + store.GetServiceAddress(config.Value.DataApi), "data/categories");
            var flowersByCategory = await getResponseAsync("http://" + store.GetServiceAddress(config.Value.DataApi), "data/flowers/" + catName);

            var vm = new CategoriesViewModel();

            vm.Categories = JsonConvert.DeserializeObject<List<Category>>(categoires);
            var activeCategory = vm.Categories.FirstOrDefault(p => p.Name == catName);
            activeCategory.Selected = "active";

            vm.FlowerList = JsonConvert.DeserializeObject<List<Flower>>(flowersByCategory);

            return PartialView("Index", vm);
        }

        public IActionResult Error()
        {
            return PartialView();
        }

        private async Task<string> getResponseAsync(string baseUrl, string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return String.Empty;
                }
            }
        }
    }
}
