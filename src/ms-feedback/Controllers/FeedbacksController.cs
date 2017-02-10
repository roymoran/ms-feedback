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
        public IActionResult New()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
