using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2.Models;
using Project2.Models.Entities;
using Project2.Services;
using Project2.Services.Interfaces;

namespace Project2.Controllers
{
    /// <summary>
    /// ShoppinfCart controler manages functions to add an item to shopping cart
    /// remove an item from shopping cart
    /// empty shoppings, check out items from shopping cart
    /// </summary>
    [Authorize(Roles ="Shopper")]
    public class ShoppingCartController : Controller
    {
        private ICartRepository _cart;
        private IItemRepository _item;
        private IUserRepository _user;
        private ICartItemRepository _cartItem;
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// inject repositories into controller
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="item"></param>
        /// <param name="user"></param>
        /// <param name="userManager"></param>
        /// <param name="cartItem"></param>
        public ShoppingCartController(ICartRepository cart, 
                                      IItemRepository item, 
                                      IUserRepository user, 
                                      UserManager<ApplicationUser> userManager, 
                                      ICartItemRepository cartItem)
        {
            _cart = cart;
            _item = item;
            _user = user;
            _userManager = userManager;
            _cartItem = cartItem;
        }


        /// <summary>
        /// ShoppinfCart/Indes displays all items in current user's shopping cart
        /// On the view, there are options to remove and item from shopping cart
        /// completely empty shopping cart
        /// and go to check with items in cart
        /// </summary>
        /// <returns>view with CartItem as the model</returns>
        public IActionResult Index()
        {
            // get the current logged in user
            var userId = _userManager.GetUserId(HttpContext.User);
            // read the shopping cart of the current user
            var shoppingCart = _cart.ReadShoppingCart(userId);
            // if shopping cart of this user is null
            // create a new shopping cart with this username
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { UserId = userId };
            }
            // read the list of CartItems by this user
            var cartItemList = _cartItem.ReadAllCartItems().Where(
                        c => c.CartId == shoppingCart.Id);
            // then display the view with a lit of CartItems
            return View(cartItemList);
        }


        /// <summary>
        /// GET BuyItem action method
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view with Item as the model</returns>
        public IActionResult BuyItem(int id)
        {
            // read the item based id
            var item = _item.ReadItem(id);
            // if item is null or item has no stock, go back to Home/Index
            if(item == null || item.AmountInStock == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            // get user id of currently logged on user
            var userId = _userManager.GetUserId(HttpContext.User);
            // read shopping cart of current user
            var shoppingCart = _cart.ReadShoppingCart(userId);
            // if the shopping cart of current user is null, create a new one based user id
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
                shoppingCart.UserId = userId;
                _cart.CreateShoppingCart(shoppingCart);
            }
            // return view with item as model
            return View(item);
        }


        /// <summary>
        /// POST BuyeItem action method
        /// reads the selected item and add it to the Item list of CartItem 
        /// with the selected quantity
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="quantity"></param>
        /// <returns>redirect to Home/Index</returns>
        [HttpPost, ActionName("BuyItem"), ValidateAntiForgeryToken]
        public IActionResult BuyConfirmed([Bind("Id", "quantity")]int Id, int quantity)
        {
            // read the selected item with id
            var item = _item.ReadItem(Id);
            // if squantity to buy is greater than amount of item in stock
            // display error message and go back to Home/Index
            if (item.AmountInStock < quantity)
            { 
                TempData["Error"] = "Not enough product in stock!\nPlease reduce number to buy.";
                return RedirectToAction("Index", "Home");
            }
            // read the currently logged on user id
            var userId = _userManager.GetUserId(HttpContext.User);
            // read the shopping cart associated with current user
            var shoppingCart = _cart.ReadShoppingCart(userId);
            // read all read CartItem associated with current user and matches Item
            var cartItem = _cartItem.ReadAllCartItems().FirstOrDefault(
                    c => c.Name == item.Name && c.Type == item.Type && c.Price == item.Price);
            // if CartItem is null
            // create a new one
            if (cartItem == null)
            {
                cartItem = new CartItems();
                cartItem.Name = item.Name;
                cartItem.Type = item.Type;
                cartItem.Price = item.Price;
                cartItem.Quantity = quantity;
                cartItem.CartId = shoppingCart.Id;
                cartItem.Cart = shoppingCart;
                _cartItem.CreateCartItem(cartItem);
            }
            // if same item easists in CartItem, add selected quantity to it
            else
            {
                cartItem.Quantity += quantity;
            }
            // reduce the amount of item in stock by selected quantity
            item.AmountInStock -= quantity;
            // update item in stock
            _item.UpdateItem(item.Id, item);
            // redirect to Home/Index
            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// GET Remove action method read cart with its id and return the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view with CartItem as model</returns>
        public IActionResult Remove(int id)
        {
            var cartItem = _cartItem.ReadCartItem(id);
            return View(cartItem);
        }


        /// <summary>
        /// POST Delete action method read CartItem and item
        /// add quantity in cart back to stock, update item in stock
        /// then delete item from CartItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Remove"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // read Cartitem
            var cartItem = _cartItem.ReadCartItem(id);
            // read item
            var item = _item.ReadAllItems().FirstOrDefault(
                    c => c.Name == cartItem.Name && c.Type == cartItem.Type && c.Price == cartItem.Price);
            // add quantity in cart back to amount of item in stock
            item.AmountInStock += cartItem.Quantity;
            // update item in stock to match the updated amount
            _item.UpdateItem(item.Id, item);
            // remove item from CartItem
            _cartItem.DeleteCartItem(id);
            // redirect to ShoppingCart/Item
            return RedirectToAction("Index", "ShoppingCart");
        }


        /// <summary>
        /// GET Empty action method
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Empty()
        {
            return View();
        }


        /// <summary>
        /// POST Empty action method ask if user is sure about emptying cart
        /// if user comfirms, empty cart, add items back in stock
        /// </summary>
        /// <returns>redirect to ShoppingCart/Index</returns>
        [HttpPost, ActionName("Empty"), ValidateAntiForgeryToken]
        public IActionResult EmptyConfirmed()
        {
            // read all items in current shopping cart
            var cartList = _cartItem.ReadAllCartItems();
            // for each item in cart, add quantity back in stock
            // update items to macth their new amounts
            // delete items from cart
            foreach (var cartItem in cartList)
            {
                var item = _item.ReadAllItems().FirstOrDefault(
                        c => c.Name == cartItem.Name && c.Type == cartItem.Type && c.Price == cartItem.Price);
                item.AmountInStock += cartItem.Quantity;
                _item.UpdateItem(item.Id, item);
                _cartItem.DeleteCartItem(cartItem.Id);
            }
            // redirect to ShoppingCart/Index
            return RedirectToAction("Index", "ShoppingCart");
        }


        /// <summary>
        /// GET Checkout action method
        /// </summary>
        /// <returns>view with Cartitem as model</returns>
        public IActionResult Checkout()
        {
            // read all items in cart
            var cartItemList = _cartItem.ReadAllCartItems();
            return View(cartItemList);
        }


        /// <summary>
        /// POST Checkout asks if user is sure about checking out
        /// if user confirms, go to check
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("Checkout"), ValidateAntiForgeryToken]
        public IActionResult CheckoutConfirmed()
        {
            // read all items in cart
            var cartList = _cartItem.ReadAllCartItems();
            // delete all items in cart
            foreach (var cartItem in cartList)
            {
                _cartItem.DeleteCartItem(cartItem.Id);
            }
            // return back to ShoppingCart/index
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}