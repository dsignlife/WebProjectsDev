using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoolBooksProject.Models;
using Microsoft.AspNet.Identity;

namespace CoolBooksProject.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private CoolBooksDbModel db = new CoolBooksDbModel();

        // GET: Reviews
        public ActionResult Index()
        {
            ViewBag.CurrentUser = User.Identity.GetUserId();
            var reviews = db.Reviews.Include(r => r.AspNetUsers).Include(r => r.Books);
            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviews reviews = db.Reviews.Find(id);
            if (reviews == null)
            {
                return HttpNotFound();
            }
            return View(reviews);
        }

        //// GET: Reviews/Create
        public ActionResult Create(int bookId)
        {
            Books book = db.Books.SingleOrDefault(b => b.Id == bookId);

            Reviews review = new Reviews
            {
                BookId = bookId,
                Title = book.Title,
                Created = DateTime.Now.Date,
                UserId = User.Identity.GetUserId(),
                IsDeleted = false
            };
            return View(review);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookId,UserId,Title,Text,Rating,Created,IsDeleted")] Reviews reviews)
        {

            if (ModelState.IsValid)
            {
                Books book = db.Books.SingleOrDefault(b => b.Id == reviews.BookId);
                book.Reviews.Add(reviews);
                db.SaveChanges();
                return RedirectToAction("Details", "Home", new { id = book.Id });
            }

            return View();
                     
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviews reviews = db.Reviews.Find(id);

            if (reviews == null)
            {
                return HttpNotFound();
            }

            // A user can only edit his/her own reviews
            if (reviews.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Home");
            }

            return View(reviews);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookId,UserId,Title,Text,Rating,Created,IsDeleted")] Reviews reviews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Home", new { id = reviews.BookId });
            }
            return View("Error");
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reviews reviews = db.Reviews.Find(id);
            if (reviews == null)
            {
                return HttpNotFound();
            }

            // A user can ONLY delete his/her own reviews
            if (reviews.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Home");
            }

            return View(reviews);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reviews reviews = db.Reviews.Find(id);
            reviews.IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
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
