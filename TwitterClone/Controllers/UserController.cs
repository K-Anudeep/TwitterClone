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
using TwitterClone.Models.Repository;

namespace TwitterClone.Controllers
{
    public class UserController : Controller
    {
        private readonly Twitter _twitter = null;
        private readonly TwitterCloneDBContext _context;
        public UserController(TwitterCloneDBContext context)
        {
            _twitter = new Twitter(context);
            _context = context;
        }

        public ActionResult Index()
        {
            List<Tweet> tweets = _twitter.TweetList();
            ViewBag.Tweets = tweets.Count;
            ViewBag.Followers = 2;
            ViewBag.Following = 3;
            return View();
        }

        public PartialViewResult TweetList()
        {
            List<Tweet> tweetList = _twitter.TweetList();
            return PartialView("TweetList",tweetList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Person session = _context.Person.SingleOrDefault(u => u.UserID == HttpContext.Session.GetString("UserID"));
            session.Active = 0;
            _twitter.Logout(session);
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

        public async Task<IActionResult> Tweet(string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                ViewBag.Uname = HttpContext.Session.GetString("UserID");
                Tweet tweet = new() { User_ID = ViewBag.Uname, Message = message, Created = DateTime.Now };
                _context.Add(tweet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.ErrMessage = "Message is Empty";
                return RedirectToAction("Index", "User");
            }
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var users = _context.Person
                .SingleOrDefault(m => m.FullName == searchName);
            if (users != null)
            {
                return View(users);
            }
            else
            {
                ViewBag.SearchFail = "No user with that name found.";
                return View("Index");
            }
        }
    }
}
