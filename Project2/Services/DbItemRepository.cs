using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models.Entities;
using Project2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    /// <summary>
    /// DbItemRepository manages Item CRUD with database
    /// </summary>
    public class DbItemRepository : IItemRepository
    {
        private ApplicationDbContext _db;
        /// <summary>
        /// inject the IItemRepository
        /// </summary>
        /// <param name="db"></param>
        public DbItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// CreateItem
        /// add a given item to the table
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Item</returns>
        public Item CreateItem(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return item;
        }


        /// <summary>
        /// ReadItem
        /// read and return an item with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Item</returns>
        public Item ReadItem(int id)
        {
            return _db.Items.FirstOrDefault(a => a.Id == id);
        }


        /// <summary>
        /// ReadAllItems
        /// read all items in the table
        /// </summary>
        /// <returns>a list of all items</returns>
        public ICollection<Item> ReadAllItems()
        {
            return _db.Items.ToList();
        }


        /// <summary>
        /// UpdateItem
        /// update an item with a given id and a new Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        public void UpdateItem(int id, Item item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }


        /// <summary>
        /// DeleteItem
        /// remove an item from table with a given id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteItem(int id)
        {
            Item item = _db.Items.Find(id);
            _db.Items.Remove(item);
            _db.SaveChanges();
        }
    }
}
