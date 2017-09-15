using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNETPT.Controllers.Web.Services;
using ASPNETPT.Models;
using ASPNETPT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ASPNETPT.Controllers.Web
{
    public class AppController : Controller

    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IBtcRepo _repo;
        private ILogger<AppController> _logger;


        public AppController(IMailService mailService, IConfigurationRoot config, IBtcRepo repo, ILogger<AppController> logger)
       {
           _mailService = mailService;
           _config = config;
            _repo = repo;
           _logger = logger;
       }
        public IActionResult Index()
        {
            try
            {
                var data = _repo.GetBtCprops().ToList();

                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed: {e.Message}");
                return Redirect("/error");

            }
          

        }

        public IActionResult Contact()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From Bitchart", model.Message);

                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";

            }
            
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

    }
}
