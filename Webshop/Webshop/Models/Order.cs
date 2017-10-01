using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Webshop.Models
{

    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderDetail> OrderLines { get; set; }

        [Display(Name = "First name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Enter your First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Enter your Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Adress 1")]
        [StringLength(50)]
        [Required(ErrorMessage = "Enter your Adress")]
        public string AddressLine1 { get; set; }


        [Display(Name = "Address 2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Enter your post code")]
        [Display(Name = "Post code")]
        [StringLength(10, MinimumLength = 4)]

        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Enter your city")]
        [StringLength(50)]
        public string City { get; set; }

        [StringLength(10)]
        public string State { get; set; }
       
        [Required(ErrorMessage = "Enter your country")]
        [StringLength(50)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Enter your phone number")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
    ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; }


        [BindNever]
        [ScaffoldColumn(false)]
        public decimal OrderTotal { get; set; }


        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderPlaced { get; set; }
    }
}
