using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBooksProject.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Subject required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "If you don't write a message, we can't help you ;)")]
        [StringLength(4096, MinimumLength = 10)]
        public string Message { get; set; }
    }
}
