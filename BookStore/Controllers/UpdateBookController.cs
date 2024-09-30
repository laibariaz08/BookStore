using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using Humanizer.Localisation;
using WEB_PROJECT.Models;

namespace BookStore.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    [Authorize(Policy = "RequireAuthenticatedUser")]

    public class UpdateBookController : Controller
    {
        private readonly ILogger<AddBookController> _logger;
        private readonly IWebHostEnvironment Env;
        private readonly IRepository<Books> repoB;
        private readonly IRepository<Genres> repoG;
        public UpdateBookController(ILogger<AddBookController> logger, IWebHostEnvironment en, IRepository<Books> Brepo, IRepository<Genres> Grepo)
        {
            _logger = logger;
            Env=en;
            this.repoB = Brepo;
            this.repoG = Grepo;
        }
        [HttpGet]
        public ActionResult UpdateBook()
        {
            BookGenre bookGenre = new BookGenre();
            bookGenre.books = repoB.GetAll();
            bookGenre.genres = repoG.GetAll();
            return View(bookGenre);
        }
        [HttpPost]
        public ActionResult UpdateBook(int bookId, string Title, string Description, float Price, int Stock, string Author_name, string Genre_Name, IFormFile xyz)
        {
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
            Books bookToUpdate = new Books()
            {
                Id = bookId,
                Title = Title,
                Description = Description,
                Price = Price,
                Stock = Stock,
                Author_name = Author_name,
                Picture = PathwithFileName
            };

            repoB.Update(bookToUpdate);

            return RedirectToAction("Admin", "Admin");
        }

    }
}
