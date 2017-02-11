using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ms_feedback.Data;
using Microsoft.AspNetCore.Mvc;
using ms_feedback.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ms_feedback.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly UserFeedbackContext _context;

        public FeedbacksController(UserFeedbackContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult New(int id)
        {
            var user = _context.Users.Find(1);
            var feedback = new Feedback();
            feedback.UserID = user.ID;
            ViewData["UserName"] = user.FullName;
            
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feedback feedback)
        {
            _context.Add(feedback);
            _context.SaveChanges();
            return RedirectToAction("New");
        }
    }
}
