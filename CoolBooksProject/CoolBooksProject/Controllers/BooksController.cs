using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    [Authorize]
    public class BooksController : Controller
    {
        private CoolBooksDbModel db = new CoolBooksDbModel();

        // GET: Books
        [Authorize]
        public ActionResult Index(string searchTerm)
        {
            string currentUserId = User.Identity.GetUserId();
            ViewBag.currentUser = currentUserId;
            ViewBag.admin = User.IsInRole("Admin");

            if (searchTerm != null)
            {
                var searchBooks = db.Books
                    .Include(b => b.AspNetUsers)
                    .Include(b => b.Authors)
                    .Include(b => b.Genres)
                    .Where(x => (x.Title.Contains(searchTerm) ||
                                  x.ISBN.Equals(searchTerm) ||
                                  x.Genres.Name.Contains(searchTerm) ||
                                  x.Authors.FirstName.Contains(searchTerm) ||
                                  x.Authors.LastName.Contains(searchTerm) ||
                                  x.AspNetUsers.Email.Contains(searchTerm)));

                return View("Index", searchBooks.OrderByDescending(i => i.UserId == currentUserId));
            }

            var books = db.Books.Include(b => b.AspNetUsers).Include(b => b.Authors).Include(b => b.Genres);
            return View("Index", books.ToList().OrderByDescending(i => i.UserId == currentUserId));
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // Check if the ISBN exists in the database when creating a book
        public JsonResult IsISBNExists(string isbn)
        {
             return Json(!db.Books.Any(n => n.ISBN == isbn && n.IsDeleted == false), JsonRequestBehavior.AllowGet);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            // Create a list of Authors' full name
            ViewBag.AuthorId = new SelectList((from a in db.Authors
                                               select new
                                               {
                                                   Id = a.Id,
                                                   FullName = a.FirstName + " " + a.LastName
                                               }), "Id", "FullName");

            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");

            CreateBookViewModel book = new CreateBookViewModel
            {
                UserId = User.Identity.GetUserId(),
                Created = DateTime.Now.Date,
                IsDeleted = false
            };

            return View(book);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,UserId,AuthorId,GenreId,Title,AlternativeTitle,Part,Description,ISBN,PublishDate,ImagePath,Created,IsDeleted")] Books book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();

                return RedirectToAction("Index", "Books");
            }

            ViewBag.AuthorId = new SelectList((from a in db.Authors
                                               select new
                                               {
                                                   Id = a.Id,
                                                   FullName = a.FirstName + " " + a.LastName
                                               }), "Id", "FullName");

            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }

            if (books.UserId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            ViewBag.AuthorId = new SelectList((from a in db.Authors
                                               select new
                                               {
                                                   Id = a.Id,
                                                   FullName = a.FirstName + " " + a.LastName
                                               }), "Id", "FullName");

            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", books.GenreId);
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,UserId,AuthorId,GenreId,Title,AlternativeTitle,Part,Description,ISBN,PublishDate,ImagePath,Created,IsDeleted")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Books");
            }

            ViewBag.AuthorId = new SelectList((from a in db.Authors
                                               select new
                                               {
                                                   Id = a.Id,
                                                   FullName = a.FirstName + " " + a.LastName
                                               }), "Id", "FullName");

            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", books.GenreId);
            return View(books);
        }

        // GET: Books/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }

            if (books.UserId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(books);
 
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Books books = db.Books.Find(id);
            books.IsDeleted = true;
            db.SaveChanges();

            return RedirectToAction("Index", "Books");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
