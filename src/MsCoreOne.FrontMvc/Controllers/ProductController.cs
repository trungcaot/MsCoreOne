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
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(ILogger<ProductController> logger,
            IProductApiClient productApiClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _productApiClient = productApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int productId)
        {
            ViewData["BackendUrl"] = _configuration.GetValue<string>("FrontMvc:BackendUrl");

            var product = await _productApiClient.GetProductById(productId);

            return View(product);
        }
    }
}
