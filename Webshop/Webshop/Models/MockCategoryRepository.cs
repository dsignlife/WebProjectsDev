using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class MockCategoryRepository : ICategoryRepository

    {
        public IEnumerable<Category> Categories
        {
            get
            {
                return new List<Category>
                {
                    new Category{CategoryId=1, CategoryName="CATEGORY1", Description="CATEGORY1"},
                    new Category{CategoryId=2, CategoryName="CATEGORY2", Description="CATEGORY2"},
                    new Category{CategoryId=3, CategoryName="CATEGORY3", Description="CATEGORY3"}
                };
            }
        }
    }
}
