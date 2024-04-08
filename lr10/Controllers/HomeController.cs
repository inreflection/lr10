using lr10.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace lr10.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ConsultationFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            ViewBag.Message = "Ви успішно зареєструвалися на консультацію!";
            return View();
        }
    }
}
