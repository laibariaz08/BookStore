//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Identity.Client;
//using System.Diagnostics;
//using System.Runtime.InteropServices;
//using WEB_PROJECT.Models;
//namespace WEB_PROJECT.Controllers
//{
//    public class AddAuthorController : Controller
//    {
//        public ActionResult AddAuthor()
//        {
//            return View();
//        }
//        [HttpPost]
//        public IActionResult AddAuthor(string Name,string bio, string nationality,string picture)
//        {
//            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CheckDB;Trusted_Connection=True;MultipleActiveResultSets=true";
//            Author a = new Author() { Name = Name, Bio=bio, Nationality=nationality, Picture=picture };
//            GenericRepository<Author> repo = new GenericRepository<Author>(connectionString);
//            repo.Add(a);
//            return View();
//        }
//    }

//}
