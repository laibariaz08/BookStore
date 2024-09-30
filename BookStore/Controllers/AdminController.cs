﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    //[Authorize(Policy = "AdminAccess")]
    //[Authorize(Policy = "RequireAuthenticatedUser")]
    [Authorize(Policy = "AdminAccess")]
    [Authorize(Policy = "RequireAuthenticatedUser")]

    public class AdminController : Controller
    {
        public ActionResult Admin()
        { 
            return View();
        }

    }
}
