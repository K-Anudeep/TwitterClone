using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web.Http;
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
    public class UserController : Controller
    {
        private readonly TwitterCloneDBContext _context;

        public UserController(TwitterCloneDBContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Person session = _context.Person.SingleOrDefault(u => u.UserID == HttpContext.Session.GetString("UserID"));
            session.Active = 0;
            _context.Update(session);
            _context.SaveChanges();
            HttpContext.Session.Clear();
            return View();
        }

        public ActionResult Profile()
        {
            var users = _context.Person
                .SingleOrDefault(m => m.UserID == HttpContext.Session.GetString("UserID"));

            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Person.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserID, Password, FullName, Email")] Person users)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    users.Joined = users.Joined;
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (users.UserID == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Profile");
            }
            return View(users);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
