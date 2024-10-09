using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<UpdatedUser> _userManager;
        private readonly IRepository<Orders> repoO;
        private readonly IRepository<OrderDetails> repoOD;
        public CartController(UserManager<UpdatedUser> userManager, IRepository<Orders> repoO, IRepository<OrderDetails> repoOD) 
        {
            _userManager = userManager;
            this.repoO = repoO;
            this.repoOD = repoOD;
        }
        public IActionResult Cart()
        {
            try
            {
                List<Books> cart = new List<Books>();
                string cartJson = HttpContext.Session.GetString("Cart");
                if (!string.IsNullOrEmpty(cartJson))
                {
                    cart = JsonConvert.DeserializeObject<List<Books>>(cartJson);
                }

                return View(cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public IActionResult UpdateCart(Dictionary<string, int> quantities)
        {
            try
            {
                List<Books> cart = new List<Books>();
                string cartJson = HttpContext.Session.GetString("Cart");
                if (!string.IsNullOrEmpty(cartJson))
                {
                    cart = JsonConvert.DeserializeObject<List<Books>>(cartJson);
                }
                foreach (var item in cart)
                {
                    if (quantities.ContainsKey(item.Title))
                    {
                        item.Stock = quantities[item.Title];
                    }
                }
                cartJson = JsonConvert.SerializeObject(cart);
                HttpContext.Session.SetString("Cart", cartJson);
                return RedirectToAction("Cart");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [Authorize(Policy = "RequireAuthenticatedUser")]
        public ViewResult Checkout()
        {
            List<Books> cart = new List<Books>();
            string cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                cart = JsonConvert.DeserializeObject<List<Books>>(cartJson);
            }

            return View(cart);
        }

        public ViewResult Thankyou(String StreetAddress, String State, String City, String PostalCode)
        {
            List<Books> cart = new List<Books>();
            string cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                cart = JsonConvert.DeserializeObject<List<Books>>(cartJson);
            }
            decimal total = 0;
            foreach (var book in cart)
            {
                total = total +( book.Stock * book.Price);
            }
            Orders order = new Orders()
            {
                U_Id = _userManager.GetUserId(User),
                OrderDate = DateTime.Now,
                StreetAddress = StreetAddress,
                State = State,
                City = City,
                PostalCode = PostalCode,
                TotalAmount = total
            };
            repoO.Add(order);
            List<Orders> orders = new List<Orders>();
            orders = repoO.GetAll();
            var id = 0;
            foreach (var o in orders)
            {
                id=o.Id;
            }
            foreach (var book in cart)
            {
                OrderDetails details = new OrderDetails()
                {
                    OrderID = id,
                    BookID = book.Id,
                    Quantity = book.Stock,
                    Price = book.Price
                };
                repoOD.Add(details);
            }
            HttpContext.Session.Remove("Cart");

            return View(cart);
        }
    }
}
