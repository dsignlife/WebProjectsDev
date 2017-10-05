using CoolBooksProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolBooksProject.ViewModels
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [StringLength(256)]
        public string AlternativeTitle { get; set; }

        public short? Part { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(50)]
        [Remote("IsISBNExists", "Books", ErrorMessage = "ISBN already exists")]
        public string ISBN { get; set; }

        public DateTime? PublishDate { get; set; }

        [StringLength(512)]
        public string ImagePath { get; set; }

        public DateTime Created { get; set; }

        public bool IsDeleted { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Authors Authors { get; set; }

        public virtual Genres Genres { get; set; }

        public static implicit operator CreateBookViewModel(Books v)
        {
            throw new NotImplementedException();
        }
    }
}