using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project2.Models.Entities;
using Project2.Services.Interfaces;

namespace Project2.Controllers
{
    /// <summary>
    /// ItemController manages CRUD of Items
    /// ItemController is only accessible to Admin
    /// </summary>
    [Authorize(Roles ="Admin")]
    public class ItemController : Controller
    {
        private IItemRepository _repo;

        public ItemController(IItemRepository repo)
        {
            _repo = repo;
        }


        /// <summary>
        /// Index page displays all a lit of all items
        /// </summary>
        /// <returns>
        /// a lit of all items
        /// </returns>
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View(_repo.ReadAllItems());
        }


        /// <summary>
        /// Creat method displays the Create view
        /// </summary>
        /// <returns>
        /// Create view
        /// </returns>
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Create POST action method
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// Create view
        /// </returns>
        [Authorize(Roles ="Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Name, Type, Price, AmountInStock")]Item item)
        {
            // check to make sure that model state is valid
            // before creating a new item
            if (ModelState.IsValid)
            {
                _repo.CreateItem(item);
                return RedirectToAction("Index", "Item");
            }
            return View();
        }


        /// <summary>
        /// Details action method
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Details view with item as model
        /// </returns>
        public IActionResult Details(int id)
        {
            var item = _repo.ReadItem(id);
            // if item is null, redirect to Item/Index
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            // If item is valid, return the view
            return View(item);
        }


        /// <summary>
        /// Edit action method
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Edit view with item as model
        /// </returns>
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int id)
        {
            var item = _repo.ReadItem(id);
            // if item is null, redirect to Item/Index
            if (item == null)
            {
                return RedirectToAction("Index", "Item");
            }
            // If item is valid, return the view
            return View(item);
        }


        /// <summary>
        /// POST Edit action method
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// View with item as model
        /// </returns>
        [Authorize(Roles ="Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id, Name, Type, Price, AmountInStock")]Item item)
        {
            // check to make sure model state is valid before editing item
            if (ModelState.IsValid)
            {
                _repo.UpdateItem(item.Id, item);
                return RedirectToAction("Index", "Item");
            }
            return View(item);
        }


        /// <summary>
        /// Delete action method
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Delete view with item as model
        /// </returns>
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            // take in the id of item and read that item
            var item = _repo.ReadItem(id);
            // if item is null, redirect to Item/Index
            if (item == null)
            {
                return RedirectToAction("Index", "Item");
            }
            // If item is valid, return the view
            return View(item);
        }


        /// <summary>
        /// POST Delete action method
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>
        /// redirects to Home/Index
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([Bind("Id, Name, Type, Price, AmountInStock")]int Id)
        {
            _repo.DeleteItem(Id);
            return RedirectToAction("Index", "Item");
        }
    }

}