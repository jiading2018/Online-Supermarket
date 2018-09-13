using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    /// <summary>
    /// DBCartItemRepository manages CartItem CRUD with database
    /// </summary>
    public class DbCartItemRepository: ICartItemRepository
    {
        private ApplicationDbContext _db;
        /// <summary>
        /// inject ICartItemRepository
        /// </summary>
        /// <param name="db"></param>
        public DbCartItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// CreateCartItem 
        /// add given Cartitem to table
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns>CartItems</returns>
        public CartItems CreateCartItem(CartItems cartItem)
        {
            _db.CartItems.Add(cartItem);
            _db.SaveChanges();
            return cartItem;
        }


        /// <summary>
        /// ReadCartItem 
        /// read the first item in cart with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CartItems</returns>
        public CartItems ReadCartItem(int id)
        {
            return _db.CartItems.FirstOrDefault(i => i.Id == id);
        }


        /// <summary>
        /// ReadAllCartItems 
        /// read and return a list of CartItemd in cart
        /// </summary>
        /// <returns>a list of CartItems</returns>
        public ICollection<CartItems> ReadAllCartItems()
        {
            return _db.CartItems.ToList();
        }


        /// <summary>
        /// UpdateCartItem with cart id and a new CartItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cartItem"></param>
        public void UpdateCartItem(int id, CartItems cartItem)
        {
            _db.Entry(cartItem).State = EntityState.Modified;
            _db.SaveChanges();
        }

        /// <summary>
        /// DeleteCartitem
        /// delete a Cartitem with a given id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCartItem(int id)
        {
            CartItems cartItem = _db.CartItems.Find(id);
            _db.CartItems.Remove(cartItem);
            _db.SaveChanges();
        }
    }
}
