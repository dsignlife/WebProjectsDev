using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoolBooksProject.Models;
using CoolBooksProject.ViewModels;
using Microsoft.AspNet.Identity;

namespace CoolBooksProject.Controllers
{
    public class HomeController : Controller
    {
        private CoolBooksDbModel db = new CoolBooksDbModel();

        public ActionResult GetProfileImage(string userId)
        {
            Users user = db.Users.FirstOrDefault(u => u.UserId == userId);
            byte[] imageData = user.Picture;

            if (imageData != null)
                return File(imageData, "image/png");
            else
            {
                return File("/Images/noimage.png", "image/png");
            }
            
        }

        public ActionResult Index()
        {
            IEnumerable<Books> books;

            books = db.Books.Include(b => b.Authors).Include(b => b.Genres).OrderByDescending(i => i.Id).Take(3); // take 3 
            return View(new ListViewModel
            {
                Books = books
            });
        }

        public ActionResult List(string genre)
        {
            IEnumerable<Books> books;
            string currentGenre = string.Empty;

            if (string.IsNullOrEmpty(genre))
            {
                books = db.Books
                    .Include(b => b.Authors)
                    .Include(b => b.Genres)
                    .OrderBy(b => b.Id);
                currentGenre = "All Books";
            }
            else
            {
                books = db.Books
                    .Where(g => g.Genres.Name == genre)
                    .OrderBy(i => i.Id);

                currentGenre = db.Genres.FirstOrDefault(d => d.Name == genre).Name;
            }


            return View(new ListViewModel
            {

                Books = books,
                CurrentGenre = currentGenre
            });
        }

        public ActionResult Details(int id)
        {

            ViewBag.CurrentUser = User.Identity.GetUserId();
            var books = db.Books.Include(r => r.Reviews).Include(a => a.AspNetUsers)
                  .Where(p => p.Id == id).Take(1).FirstOrDefault();

            if (books == null)
            {
                return View("ERROR 404");
            }

            return View(books);
        }

        public ActionResult About()
        {
            string aboutText = "This appliction serves as a CoolBookReviewer";

            ViewBag.Message = aboutText;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}