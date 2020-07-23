using Newtonsoft.Json;

namespace MsCoreOne.Application.Products.Queries.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public string ThumbnailImageUrl { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

    }
}
