using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
namespace BookStore.Controllers
{

    public class DashBoardController : Controller
    {
        private readonly IRepository<Books> repo;
        private readonly IRepository<Genres> repo_category;
        public DashBoardController(IRepository<Books> r, IRepository<Genres> repo_category)
        {
            repo = r;
            this.repo_category = repo_category;
        }
        [HttpGet]
        public ViewResult Dashboard()
        {
            BookGenre BG = new BookGenre();
            BG.books = repo.GetAll().ToList();
            BG.genres = repo_category.GetAll().ToList();
            return View(BG);
        }

        [HttpPost]
        public ViewResult Dashboard(int BoohId, string BoohTitle, decimal Price, string Picture, int Quantity)
        {
            bool f = false;
            Books b = new Books();
            List<Books> cart;
            string cartJson = HttpContext.Session.GetString("Cart");

            // Retrieve cart from session
            if (!string.IsNullOrEmpty(cartJson))
            {
                cart = JsonConvert.DeserializeObject<List<Books>>(cartJson);
            }
            else
            {
                cart = new List<Books>();
            }
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id == BoohId)
                {
                    cart[i].Stock += Quantity;//While adding product in cart i am using stoch for the quantity of that product because there is no attribute to in products table for quantity and we have to store the quantity of product ordered so i am using Stoch for this.
                    b = cart[i];
                    f = true;
                    cartJson = JsonConvert.SerializeObject(cart);
                    HttpContext.Session.SetString("Cart", cartJson);
                    break;
                }
            }
            if (!(f))
            {
                b = new Books
                {
                    Id = BoohId,
                    Title = BoohTitle,
                    Price = Price,
                    Stock = Quantity,
                    Picture = Picture
                };
                cart.Add(b);
                cartJson = JsonConvert.SerializeObject(cart);
                HttpContext.Session.SetString("Cart", cartJson);
            }
            BookGenre BG = new BookGenre();
            BG.books = repo.GetAll().ToList();
            BG.genres = repo_category.GetAll().ToList();
            return View(BG);
        }


        public ActionResult DetailProduct(int id)
        {
            Books book = repo.GetById(id);
            return View(book);
        }

        public IActionResult Search(string inputData)
        {
            List<Books> b;
            b = repo.Search(inputData);
            return View("Search", b);
        }

    }
}