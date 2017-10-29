using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Webshop.Models
{
    public class DbInitializer
    {
        private static Dictionary<string, Category> categories;

        public static Dictionary<string, Category> Categories {
            get
            {
                if (categories == null) {
                    var genresList = new[] {
                        new Category { CategoryName = "Type1" },
                        new Category { CategoryName = "Type2" },
                        new Category { CategoryName = "Type3" }
                    };
                    categories = new Dictionary<string, Category>();
                    foreach (var genre in genresList)
                        categories.Add(genre.CategoryName, genre);
                }
                return categories;
            }
        }

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            var context = applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();
            if (!context.Categories.Any())
                context.Categories.AddRange(Categories.Select(c => c.Value));

            //add mock data to database
            if (!context.Products.Any())
                context.AddRange
                (
                    new Product {
                        Name = "",
                        Price = 15.95M,
                        ShortDesc = "",
                        LongDesc = "",
                        Category = Categories["Type1"],
                        ImageUrl = "url",
                        InStock = true,
                        IsProductOfTheWeek = false,
                        //ImageThumbnailUrl = "",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "",
                        Price = 14.20M,
                        ShortDesc = "You'll love it!",
                        LongDesc = "",
                        Category = Categories["Type2"],
                        ImageUrl = "",
                        InStock = false,
                        IsProductOfTheWeek = false,
                        //ImageThumbnailUrl = "",
                        ExtraInfo = ""
                    }
                );
            context.SaveChanges();
        }
    }
}