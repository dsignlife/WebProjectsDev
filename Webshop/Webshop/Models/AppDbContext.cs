﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Webshop.Models
{
    public class AppDbContext: DbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
