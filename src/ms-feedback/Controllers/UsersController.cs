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
    public class UsersController : Controller
    {
        private readonly UserFeedbackContext _context;

        public UsersController(UserFeedbackContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult New()
        {
            var user = new User();
            user.ID = Guid.NewGuid();
            //Guid g;
            // Create and display the value of two GUIDs.
            //g = Guid.NewGuid();
            //Console.WriteLine(g);
            //Console.WriteLine(Guid.NewGuid());
            return View(user);
        }

        public IActionResult Create(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return RedirectToAction("New");
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Show()
        {
            return View(_context.Users.ToList());
        }

        public IActionResult Destroy()
        {
            return View();
        }
    }
}
