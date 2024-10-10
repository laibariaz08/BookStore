using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BookStore.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    [Authorize(Policy = "RequireAuthenticatedUser")]
    public class AddBookController : Controller
    {
        private readonly ILogger<AddBookController> _logger;
        private readonly IWebHostEnvironment Env;
        private readonly IRepository<Books> repoB;
        private readonly IRepository<Genres> repoG;

        public AddBookController(ILogger<AddBookController> logger, IWebHostEnvironment en, IRepository<Books> Brepo, IRepository<Genres> Grepo)
        {
            _logger = logger;
            Env = en;
            this.repoB = Brepo;
            this.repoG = Grepo;
        }
        [HttpGet]
        public ActionResult AddBook()
        {
            List<Genres> g = new List<Genres>();
            g = repoG.GetAll().ToList();
            return View(g);
        }


        [HttpPost]
        public ActionResult AddBook(string Title, string genre_name, decimal Price, string Description, int Stock, string Author, IFormFile xyz)
        {
            Console.WriteLine("In AddBook Controller");
            Books book  = new Books();
            book.Title = Title;
            book.Price = Price;
            book.Stock = Stock;
            book.genre_Name = genre_name;
            book.Description = Description;
            book.Author_name = Author;
            string wwwFolder = Env.WebRootPath;
            string path = Path.Combine(wwwFolder, "UploadedImages");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = Path.GetFileName(xyz.FileName);
            var PathwithFileName = Path.Combine(path, filename);
            FileStream stream = new FileStream(PathwithFileName, FileMode.Create);
            xyz.CopyTo(stream);
            stream.Close();
            int index = PathwithFileName.IndexOf("Uploaded");
            PathwithFileName = PathwithFileName.Substring(index);
            PathwithFileName = PathwithFileName.Replace('\\', '/');
            PathwithFileName = "/" + PathwithFileName;
            book.Picture = PathwithFileName;
            repoB.Add(book);
            List<Genres> g = new List<Genres>();
            g = repoG.GetAll().ToList();
            return View(g);
        }

    }
}
