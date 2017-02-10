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
            return View();
        }

        public IActionResult Create()
        {
            return View();
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
