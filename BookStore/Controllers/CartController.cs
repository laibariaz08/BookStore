﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<Order> repoO;
        private readonly IRepository<OrderDetails> repoOD;
        public CartController(UserManager<IdentityUser> userManager, IRepository<Order> repoO, IRepository<OrderDetails> repoOD) 
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
                OrderDetails details = new OrderDetails()
                {
                    BookID = book.Id,
                    Quantity = book.Stock,
                    Price = book.Price
                };
                repoOD.Add(details);
            }
            Order order = new Order()
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
            List<Order> orders = new List<Order>();
            orders = repoO.GetAll();
            foreach (var o in orders)
            {

            }

            HttpContext.Session.Remove("Cart");

            return View(cart);
        }

        //[HttpPost]
        //public IActionResult ThankYou(
        //        string FullName,
        //        string Address,
        //        string City,
        //        string State,
        //        string ZipCode,
        //        decimal Total,
        //        int[] ProductIDs,
        //        int[] Quantities,
        //        decimal[] Prices)
        //{
        //    string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CheckDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        //    GenericRepository<Order> o = new GenericRepository<Order>(connectionString);
        //    List<Order> orders = new List<Order>();
        //    orders = o.GetAll().ToList();
        //    int oid = 0;
        //    foreach (var or in orders)
        //    {
        //        oid = or.ID;
        //    }
        //    oid = oid + 1;

        //    //    // Get the logged-in user's user ID (if you have it as a claim)
        //    var userId = User.Identity.IsAuthenticated
        //     ? ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value
        //     : "Anonymous";
        //    var order = new Order
        //    {
        //        ID = oid,
        //        UID = userId, // Set the current user's ID. Replace with actual user ID retrieval.
        //        Name = FullName,
        //        Address = Address,
        //        City = City,
        //        State = State,
        //        ZipCode = ZipCode,
        //        Bill = Total,
        //        OrderDate = DateTime.Now
        //    };
        //    GenericRepository<Order> repo = new GenericRepository<Order>(connectionString);
        //    repo.AddOrder(order);
        //    for (int i = 0; i < ProductIDs.Length; i++)
        //    {
        //        var orderProduct = new OrderProducts
        //        {
        //            OrderID = oid,
        //            ProductID = ProductIDs[i],
        //            Quantity = Quantities[i],
        //            Price = Prices[i],

        //        };
        //        GenericRepository<OrderProducts> repoo = new GenericRepository<OrderProducts>(connectionString);
        //        repoo.AddOrder(orderProduct);
        //    }

        //    List<Books> cart = new List<Books>();
        //    string cartJson = HttpContext.Session.GetString("Cart");
        //    if (!string.IsNullOrEmpty(cartJson))
        //    {
        //        cart = JsonConvert.DeserializeObject<List<Books>>(cartJson);
        //    }

        //    // Clear the cart after checkout
        //    HttpContext.Session.Remove("Cart");
        //    // Pass cart items to the view
        //    return View(cart);
        //}
    }
}
