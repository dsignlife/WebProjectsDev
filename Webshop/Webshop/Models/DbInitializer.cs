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
                        new Category {CategoryName = "Fruit pies"},
                        new Category {CategoryName = "Cheese cakes"},
                        new Category {CategoryName = "Seasonal pies"}
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
                        Name = "Apple Pie",
                        Price = 12.95M,
                        ShortDesc = "Our famous apple pies!",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Fruit pies"],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg",
                        InStock = true,
                        IsProductOfTheWeek = true,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "Blueberry Cheese Cake",
                        Price = 18.95M,
                        ShortDesc = "You'll love it!",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Cheese cakes"],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecake.jpg",
                        InStock = true,
                        IsProductOfTheWeek = false,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecakesmall.jpg",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "Cheese Cake",
                        Price = 18.95M,
                        ShortDesc = "Plain cheese cake. Plain pleasure.",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Cheese cakes"],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg",
                        InStock = true,
                        IsProductOfTheWeek = false,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "Cherry Pie",
                        Price = 15.95M,
                        ShortDesc = "A summer classic!",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Fruit pies"],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypie.jpg",
                        InStock = true,
                        IsProductOfTheWeek = false,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "Christmas Apple Pie",
                        Price = 13.95M,
                        ShortDesc = "Happy holidays with this pie!",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Seasonal pies"],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepie.jpg",
                        InStock = true,
                        IsProductOfTheWeek = false,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepiesmall.jpg",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "Cranberry Pie",
                        Price = 17.95M,
                        ShortDesc = "A Christmas favorite",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Seasonal pies"],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypie.jpg",
                        InStock = true,
                        IsProductOfTheWeek = false,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypiesmall.jpg",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "Peach Pie",
                        Price = 15.95M,
                        ShortDesc = "Sweet as peach",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Fruit pies"],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpie.jpg",
                        InStock = false,
                        IsProductOfTheWeek = false,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpiesmall.jpg",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "Pumpkin Pie",
                        Price = 12.95M,
                        ShortDesc = "Our Halloween favorite",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Seasonal pies"],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg",
                        InStock = true,
                        IsProductOfTheWeek = true,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "Rhubarb Pie",
                        Price = 15.95M,
                        ShortDesc = "My God, so sweet!",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Fruit pies"],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg",
                        InStock = true,
                        IsProductOfTheWeek = true,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "Strawberry Pie",
                        Price = 15.95M,
                        ShortDesc = "Our delicious strawberry pie!",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Fruit pies"],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg",
                        InStock = true,
                        IsProductOfTheWeek = false,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg",
                        ExtraInfo = ""
                    },
                    new Product {
                        Name = "Strawberry Cheese Cake",
                        Price = 18.95M,
                        ShortDesc = "You'll love it!",
                        LongDesc =
                            "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                        Category = Categories["Cheese cakes"],
                        ImageUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecake.jpg",
                        InStock = false,
                        IsProductOfTheWeek = false,
                        ImageThumbnailUrl =
                            "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecakesmall.jpg",
                        ExtraInfo = ""
                    }
                );
            context.SaveChanges();
        }
    }
}