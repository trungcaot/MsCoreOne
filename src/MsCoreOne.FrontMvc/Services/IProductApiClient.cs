using MsCoreOne.FrontMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsCoreOne.FrontMvc.Services
{
    public interface IProductApiClient
    {
        Task<IEnumerable<ProductVm>> GetProducts();

        Task<ProductVm> GetProductById(int productId);

        Task<IEnumerable<ProductVm>> GetProductsByCategoryId(int categoryId);
    }
}
