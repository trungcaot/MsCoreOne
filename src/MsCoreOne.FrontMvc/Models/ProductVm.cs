using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsCoreOne.FrontMvc.Models
{
    public class ProductVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public string ThumbnailImageUrl { get; set; }
        
    }
}
