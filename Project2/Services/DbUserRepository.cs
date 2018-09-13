using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models;

namespace Project2.Services
{
    /// <summary>
    /// DbUserRepository manages Read functino for User table
    /// </summary>
    public class DbUserRepository : IUserRepository
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// inject IUserRepository and ApplicationDbContext
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userManager"></param>
        public DbUserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        /// <summary>
        /// Readuser
        /// eager load user with profile and return user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApplicationUser ReadUser(string email)
        {
            var user = _db.Users.Include(u=> u.Profile).FirstOrDefault(u => u.Email == email);

            return user;
        }
    }
}
