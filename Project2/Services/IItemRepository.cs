using Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services.Interfaces
{
    /// <summary>
    /// IItemRepository creates CRUD interface for Item
    /// </summary>
    public interface IItemRepository
    {
        /// <summary>
        /// CreateItem
        /// add a given item to the table
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Item</returns>
        Item CreateItem(Item item);
        /// <summary>
        /// ReadItem
        /// read and return an item with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Item</returns>
        Item ReadItem(int id);
        /// <summary>
        /// ReadAllItems
        /// read all items in the table
        /// </summary>
        /// <returns>a list of all items</returns>
        ICollection<Item> ReadAllItems();
        /// <summary>
        /// UpdateItem
        /// update an item with a given id and a new Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        void UpdateItem(int id, Item item);
        /// <summary>
        /// DeleteItem
        /// remove an item from table with a given id
        /// </summary>
        /// <param name="id"></param>
        void DeleteItem(int id);
    }
}

