using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ms_feedback.Data;
using Microsoft.AspNetCore.Mvc;
using ms_feedback.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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
        public IActionResult New(Guid? uid)
        {
           if(uid == null)
            {
                return View();
            }
            var user = _context.Users.Find(uid);
           
            var feedback = new Feedback();
            feedback.UserID = (Guid)uid;
            ViewData["UserName"] = user.FullName;
            
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (feedback.FeedbackText == null)
            {
                return RedirectToAction("New", new { uid = feedback.UserID });
            }
            _context.Add(feedback);
            _context.SaveChanges();

            // trigger email with unique feedback link and message
            var user = _context.Users.Find(feedback.UserID);
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "email", user.Email },
                    { "feedback_text", feedback.FeedbackText},
                    { "first_name", user.FullName.Split(' ')[0]}
                };

                string output = JsonConvert.SerializeObject(values);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync("https://prod-12.westus.logic.azure.com:443/workflows/bf84617c86174fbebc78e9fecc99f21f/triggers/request/run?api-version=2016-06-01&sp=%2Ftriggers%2Frequest%2Frun&sv=1.0&sig=OVO2EJShxToD-1BgkAa7r6idmcUJbgCnNK4wAsf-GvQ", new StringContent(output, Encoding.UTF8, "application/json"));

                var responseString = await response.Content.ReadAsStringAsync();
            }

            return RedirectToAction("New", "Users");
        }
    }
}
