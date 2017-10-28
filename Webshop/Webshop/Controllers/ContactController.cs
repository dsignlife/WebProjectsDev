using System;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Webshop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Webshop.Controllers
{
    public class ContactController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind("Subject,Name,Email,Message")] ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress(contact.Email);
                    msg.To.Add("contact.webproject@gmail.com");
                    msg.Subject = contact.Subject;
                    msg.Body = contact.Message + "\nEmail: " + contact.Email + "\nName: " + contact.Name;

                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        Credentials = new System.Net.NetworkCredential("contact.webproject@gmail.com", "Admin=1337"),
                        EnableSsl = true
                    };

                    smtp.Send(msg);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for contacting us!";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $"Sorry, a problem occurred {ex.Message}";
                }
            }

            return View();
        }
    }
    
}