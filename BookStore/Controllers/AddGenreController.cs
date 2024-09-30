using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace BookStore.Controllers
{
    public class AddGenreController : Controller
    {
        private readonly IRepository<Genres> repoG;
        public AddGenreController(IRepository<Genres> repoG)
        {
            this.repoG = repoG;
        }
        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddGenre(string Name)
        {
            Genres category = new Genres() { Name = Name };
            repoG.Add(category);
            return RedirectToAction("Admin", "Admin");
        }
    }

}
