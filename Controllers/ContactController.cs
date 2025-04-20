using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using Portfolio.ViewModels;
using Microsoft.Extensions.Options;
using Portfolio.Settings;
using Portfolio.Helpers;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;


namespace Portfolio.Controllers
{
    public class ContactController : Controller
    {
        public ContactController(AppDbContext db , IOptions<SmtpSettings> smtpOptions , EmailService emailService)
        {
            _db = db;
            _smtpOptions = smtpOptions.Value;
            _emailService = emailService;
        }
        private readonly AppDbContext _db;
        private readonly SmtpSettings _smtpOptions;
        private readonly EmailService _emailService;

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

            // Send email
            var messageBody = $"You received a new message from the contact form:\n\n" +
                      $"Name: {model.Name}\n" +
                      $"Email: {model.Email}\n" +
                      $"Subject: {model.Subject}\n" +
                      $"Message:\n{model.Message}";
            _emailService.SendEmail("New Message From Contact Form", messageBody);


            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }

    }
}
