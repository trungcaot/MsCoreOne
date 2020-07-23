using System.Collections.Generic;

namespace MsCoreOne.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public string ImageFileName { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public IList<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
