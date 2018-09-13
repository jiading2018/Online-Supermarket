using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    /// <summary>
    /// IUserRepository creates Read Interface for User table
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Readuser
        /// eager load user with profile and return user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        ApplicationUser ReadUser(string username);
    }
}
