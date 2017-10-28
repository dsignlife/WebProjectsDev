using System.ComponentModel.DataAnnotations;

namespace Webshop.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Subject required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email required"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "ERROR"), StringLength(4096, MinimumLength = 10)]
        public string Message { get; set; }
    }
}