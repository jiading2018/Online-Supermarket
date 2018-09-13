using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project2.Models;
using Project2.Services;
using Project2.Services.Interfaces;

namespace Project2.Controllers
{

    // HomeController
    [Authorize]
    public class HomeController : Controller
    {
        private IItemRepository _repo;
        private IUserRepository _user;

        public HomeController(IItemRepository repo, IUserRepository user)
        {
            _repo = repo;
            _user = user;
        }


        /// <summary>
        /// Home/Index displays all items in the database
        /// Makes sure this page is only accessible to Shoppers
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // If current user is Admin, redirect to Item/Index
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Item");
            }
            else
            {
                // checks to see if user has a profile
                // if user does not have a profile, redirect to _profile/Create
                // if user has a profile, display the view
                if(_user.ReadUser(User.Identity.Name).Profile == null)
                {
                    return RedirectToAction("Create", "Profile");
                }
                return View(_repo.ReadAllItems());
            }            
        }


        /// <summary>
        /// About page displays information about this project
        /// and creater of this project
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Jia Ding";
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
