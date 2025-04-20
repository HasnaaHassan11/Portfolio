using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using Portfolio.ViewModels;

namespace Portfolio.Controllers
{
    public class ContactController : Controller
    {
        public ContactController(AppDbContext db)
        {
            _db = db;
        }
        private readonly AppDbContext _db;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Preview(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View("FillForm", model);

            return View("Preview", model); 
        }

        [HttpPost]
        public IActionResult Submit(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Preview", model);

            var contact = new Contact
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
                CreatedAt = DateTime.Now
            };

            _db.Contacts.Add(contact);
            _db.SaveChanges();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }

    }
}
