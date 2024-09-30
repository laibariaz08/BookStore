using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    [Authorize(Policy = "RequireAuthenticatedUser")]
    public class DeleteBookController : Controller
    {
        private readonly IRepository<Books> repoB;

        public DeleteBookController(IRepository<Books> Brepo)
        {
            this.repoB = Brepo;
        }
        [HttpGet]
        public ActionResult DeleteBook()
        {
            List<Books> books = repoB.GetAll();
            return View(books);
        }
        [HttpPost]
        public ActionResult DeleteBook(int ID)
        {
            
            repoB.Delete(ID);
            List<Books> books = repoB.GetAll();
            return View(books);
        }
    }
}
