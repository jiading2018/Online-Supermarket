using Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    /// <summary>
    /// ICartRepository create Cread Read interface for ShoppingCart
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        /// CreateShoppingCart
        /// add a given ShoppingCart to the table
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns>ShoppingCart</returns>
        ShoppingCart CreateShoppingCart(ShoppingCart shoppingCart);
        /// <summary>
        /// ReadShoppingCart
        /// read the shopping cart of a given user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ShoppingCart ReadShoppingCart(string applicationUserId);
    }
}
