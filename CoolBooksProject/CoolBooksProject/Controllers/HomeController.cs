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
        
        public Books RandomBook()
        {
            Books randomBook;
            Random rnd = new Random(DateTime.Now.Millisecond);

            List<int> bookIds = new List<int>();
            foreach (var book in db.Books)
            {
                if (book.IsDeleted == false)
                    bookIds.Add(book.Id);
            }

            int randomIndex = rnd.Next(bookIds.Count());
            int randomBookId = bookIds[randomIndex];
            randomBook = db.Books.FirstOrDefault(x => x.Id == randomBookId);

            return randomBook;
        }

        public ActionResult Index()
        {
            // Setup a random book to be shown on front page
            Books randomBook = RandomBook();
            Authors randomBookAuthor = db.Authors.FirstOrDefault(a => a.Id == randomBook.AuthorId);

            ViewBag.RandomBookImagePath = randomBook.ImagePath;
            ViewBag.RandomBookTitle = randomBook.Title;
            ViewBag.RandomBookAuthor = randomBookAuthor.FirstName + " " + randomBookAuthor.LastName;
            ViewBag.RandomBookId = randomBook.Id;
            ViewBag.RandomBookDescription = randomBook.Description;

            // View the latest 3 added books
            IEnumerable<Books> Latestbooks;
            Latestbooks = db.Books.Where(b => b.IsDeleted == false)
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .OrderByDescending(i => i.Id).Take(3);

            return View(new ListViewModel
            {
                Books = Latestbooks
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
            ViewBag.admin = User.IsInRole("Admin");
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