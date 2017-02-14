using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ms_feedback.Data;
using Microsoft.AspNetCore.Mvc;
using ms_feedback.Models;
using System.Security.Cryptography;
using Microsoft.AspNet.Cryptography.KeyDerivation;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            //Should add unqiueness constraint on email
            //Should lowercase email before saving

            //commenting out password since this is being removed.
            //There currently isn't a reason for registered users to login
            // generate a 128-bit salt using a secure PRNG
            /*byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            user.PasswordHashed = hashed;
            user.PasswordSalt = salt;
            user.Password = "";
            user.PasswordConfirmed = "";*/
            if (user.Email == null || user.FullName == null)
            {
                return RedirectToAction("New");
            }
            user.ID = Guid.NewGuid();
            _context.Add(user);
            _context.SaveChanges();
           
            // trigger email with unique feedback link and message
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "email", user.Email },
                    { "feedback_link", "http://smartfeedback.azurewebsites.net/Feedbacks/New?uid=" + user.ID.ToString()},
                    { "first_name", user.FullName.Split(' ')[0]}
                };

                string output = JsonConvert.SerializeObject(values);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync("https://prod-23.westus2.logic.azure.com:443/workflows/20f19b0072834a63a0f1ed8aa7316944/triggers/request/run?api-version=2016-06-01&sp=%2Ftriggers%2Frequest%2Frun&sv=1.0&sig=wYCQdY1g8RpFu8GEWcz8W_KyPc1-a530BolouqqwAK8", new StringContent(output, Encoding.UTF8, "application/json"));

                var responseString = await response.Content.ReadAsStringAsync();
            }

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
