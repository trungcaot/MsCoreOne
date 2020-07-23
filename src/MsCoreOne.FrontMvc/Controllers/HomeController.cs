using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MsCoreOne.FrontMvc.Models;
using MsCoreOne.FrontMvc.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MsCoreOne.FrontMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger,
            IProductApiClient productApiClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _productApiClient = productApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            ViewData["BackendUrl"] = _configuration.GetValue<string>("FrontMvc:BackendUrl");

            var products = Enumerable.Empty<ProductVm>();
            if (categoryId == null)
            {
                _logger.LogInformation("GetProducts called.");
                products = await _productApiClient.GetProducts();
            }
            else
            {
                _logger.LogInformation($"GetProductsByCategoryId called with categoryId: {categoryId.Value}.");
                products = await _productApiClient.GetProductsByCategoryId(categoryId.Value);
            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
