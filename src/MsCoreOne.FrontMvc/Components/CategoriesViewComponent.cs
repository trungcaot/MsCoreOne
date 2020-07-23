using Microsoft.AspNetCore.Mvc;
using MsCoreOne.FrontMvc.Services;
using System.Threading.Tasks;

namespace MsCoreOne.FrontMvc.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public CategoriesViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApiClient.GetCategories();

            return View(categories);
        }
    }
}
