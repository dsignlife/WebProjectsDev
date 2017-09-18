using System.Collections.Generic;

namespace Webshop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }


    }
}