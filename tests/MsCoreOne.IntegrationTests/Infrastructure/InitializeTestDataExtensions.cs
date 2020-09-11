using MsCoreOne.Domain.Entities;
using MsCoreOne.Infrastructure.Identity;
using MsCoreOne.Infrastructure.Persistence;
using MsCoreOne.IntegrationTests.Common.Data;
using System.Collections.Generic;

namespace MsCoreOne.IntegrationTests.Infrastructure
{
    public static class InitializeTestDataExtensions
    {
        public static ApplicationDbContext InitializeTestDatabase(this ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser
            {
                Id = MasterData.UserId.ToString(),
                UserName = "test",
                Email = "test@gmail.com"
            });

            var category = new Category { Id = 1, Name = "Category" };
            var brand = new Brand { Id = 1, Name = "Brand" };

            context.Categories.Add(category);

            context.Brands.Add(brand);

            var product = new Product
            {
                Name = "Product",
                Price = 1000m,
                BrandId = 1,
                Brand = brand
            };

            product.ProductCategories = new List<ProductCategory>
            {
                new ProductCategory
                {
                    Product = product,
                    Category = category
                }
            };

            context.Products.Add(product);

            var country = new Country { Id = 1, Name = "Viet Nam", SortName = "84", PhoneCode = "84" };
            context.Countries.Add(country);

            context.SaveChanges();

            return context;
        }
    }
}
