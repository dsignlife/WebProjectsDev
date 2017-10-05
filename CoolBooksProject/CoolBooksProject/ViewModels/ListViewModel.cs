using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBooksProject.Models;

namespace CoolBooksProject.ViewModels
{
    public class ListViewModel
    {
        public IEnumerable<Books> Books { get; set; }
    }
}
