using MsCoreOne.Domain.Entities;
using MsCoreOne.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace MsCoreOne.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser {
                FirstName = "One",
                Lastname = "MsCore",
                UserName = "admin@mscoreone.com", 
                Email = "admin@mscoreone.com" 
            };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "P@ssw0rd");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category { Name = "Phones" });
                context.Categories.Add(new Category { Name = "Tablets" });
                context.Categories.Add(new Category { Name = "Computers" });
                context.Categories.Add(new Category { Name = "Accessories" });

                await context.SaveChangesAsync();
            }

            if (!context.Brands.Any())
            {
                context.Brands.Add(new Brand { Name = "Mac" });
                context.Brands.Add(new Brand { Name = "Samsung" });
                context.Brands.Add(new Brand { Name = "Nokia" });

                context.Brands.Add(new Brand { Name = "Lenovo" });
                context.Brands.Add(new Brand { Name = "Dell" });
                context.Brands.Add(new Brand { Name = "Asus" });
                context.Brands.Add(new Brand { Name = "HP" });

                await context.SaveChangesAsync();
            }
        }
    }
}
