using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Models;
using TwitterClone.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TwitterClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TwitterCloneDBContext _context;

        public HomeController(ILogger<HomeController> logger, TwitterCloneDBContext context)
        {
            _logger = logger;
            _context = context;

        }

        //GET: Home/UserLogin
        public ActionResult UserLogin()
        {
            return View();
        }


        //POST: Home/UserLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(Person userLogin)
        {
            var login = _context.Person.Where(a => a.UserID.Equals(userLogin.UserID)
                                              && a.Password.Equals(userLogin.Password)).FirstOrDefault();
            if (login != null)
            {
                Person session = _context.Person.SingleOrDefault(u => u.UserID == userLogin.UserID);
                session.Active = 1;
                HttpContext.Session.SetString("FullName", login.FullName);
                HttpContext.Session.SetString("UserID", login.UserID);
                _context.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.ErrMsg = "Invalid Credentials";
                return View();
            }           
        }

        // GET: Home/Signup
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Home/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("UserID, Password, FullName, Email, Joined")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Active = 0;
                person.Joined = DateTime.Now;
                _context.Person.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserLogin");
            }
            return View(person);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
