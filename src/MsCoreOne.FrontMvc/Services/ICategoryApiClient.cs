using MsCoreOne.FrontMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsCoreOne.FrontMvc.Services
{
    public interface ICategoryApiClient
    {
        Task<IEnumerable<CategoryVm>> GetCategories();
    }
}
