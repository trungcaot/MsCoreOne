using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsCoreOne.FrontMvc.Components
{
    public class StarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int rating)
        {
            return View(rating);
        }
    }
}
