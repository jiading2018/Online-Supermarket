using Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    /// <summary>
    /// ICartItemRepository creates CRUD Interface for CartItem
    /// </summary>
    public interface ICartItemRepository
    {
        /// <summary>
        /// CreateCartItem 
        /// add given Cartitem to table
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns>CartItems</returns>
        CartItems CreateCartItem(CartItems cartItem);
        /// <summary>
        /// ReadCartItem 
        /// read the first item in cart with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CartItems</returns>
        CartItems ReadCartItem(int id);
        /// <summary>
        /// ReadAllCartItems 
        /// read and return a list of CartItemd in cart
        /// </summary>
        /// <returns>a list of CartItems</returns>
        ICollection<CartItems> ReadAllCartItems();
        /// <summary>
        /// UpdateCartItem with cart id and a new CartItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cartItem"></param>
        void UpdateCartItem(int id, CartItems cartItem);
        /// <summary>
        /// DeleteCartitem
        /// delete a Cartitem with a given id
        /// </summary>
        /// <param name="id"></param>
        void DeleteCartItem(int id);
    }
}
