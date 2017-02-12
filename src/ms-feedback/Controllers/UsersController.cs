using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ms_feedback.Data;
using Microsoft.AspNetCore.Mvc;
using ms_feedback.Models;
using System.Security.Cryptography;
using Microsoft.AspNet.Cryptography.KeyDerivation;

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
        public IActionResult Create(User user)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
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
            user.ID = Guid.NewGuid();
            user.Password = "";
            user.PasswordConfirmed = "";
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
