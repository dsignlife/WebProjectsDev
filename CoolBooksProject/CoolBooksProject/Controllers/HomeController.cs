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
using System.Net.Mail;

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

            // Get top-ranked books
            var HighestAvgReviews = db.Reviews.Where(r => !r.IsDeleted)
                .GroupBy(r => new { ID = r.BookId })
                .Select(g => new
                {
                    ID = g.Key.ID,
                    Average = g.Average(p => p.Rating)
                });
            var FiveTop = HighestAvgReviews.OrderByDescending(q => q.Average).Take(5).ToList();
            List<Books> FiveTopBooks = new List<Books>();
            var FiveTopRating = new List<double>();

           
            foreach (var item in FiveTop)
            {
                
                FiveTopBooks.Add(db.Books.Find(item.ID));
                FiveTopRating.Add(Math.Round(Convert.ToDouble(item.Average),1)); // one decimal
            }

            


            ViewBag.TopBooks = FiveTopBooks;
            ViewBag.FiveTopRating = FiveTopRating;


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
                .OrderByDescending(i => i.Id).Take(5);

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

                    .Where(r => !r.IsDeleted)
                    .Include(b => b.Authors)
                    .Include(b => b.Genres)
                    .Include(b => b.Reviews)
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

            var rating = Math.Round(Convert.ToDouble(db.Reviews.Where(r => r.BookId == id && !r.IsDeleted)
          .Average(r => r.Rating)));
            ViewBag.Rating = rating;

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
            string aboutText = "This application serves as a CoolBookReviewer";

            ViewBag.Message = aboutText;

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact([Bind(Include = "Subject,Name,Email,Message")] ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress(contact.Email);
                    msg.To.Add("contact.coolbooks@gmail.com");
                    msg.Subject = contact.Subject;
                    msg.Body = contact.Message+ "\nEmail: " + contact.Email + "\nName: " + contact.Name;

                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        Credentials = new System.Net.NetworkCredential("contact.coolbooks@gmail.com", "Admin==1337"),
                        EnableSsl = true
                    };

                    smtp.Send(msg);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for contacting us!";
                }
                catch(Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $"Sorry, a problem occurred {ex.Message}";
                }
            }

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}