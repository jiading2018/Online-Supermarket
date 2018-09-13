using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models;
using Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    /// <summary>
    /// DbCartRepository manages Create and Read for ShoppingCarts table
    /// </summary>
    public class DbCartRepository:ICartRepository
    {
        private ApplicationDbContext _db;
        /// <summary>
        /// inject the ICartRepository
        /// </summary>
        /// <param name="db"></param>
        public DbCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// CreateShoppingCart
        /// add a given ShoppingCart to the table
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns>ShoppingCart</returns>
        public ShoppingCart CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Add(shoppingCart);
            _db.SaveChanges();
            return shoppingCart;
        }


        /// <summary>
        /// ReadShoppingCart
        /// read the shopping cart of a given user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ShoppingCart ReadShoppingCart(string userId)
        {
            return _db.ShoppingCarts.FirstOrDefault(s => s.UserId == userId);
        }
    }
}
