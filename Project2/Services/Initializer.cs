using Project2.Data;
using Project2.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    public class Initializer
    {
        private ApplicationDbContext _context;
        private RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;


        /// <summary>
        /// injects ApplicationDbContext, RoleManager, and UserManager
        /// </summary>
        /// <param name="context"></param>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        public Initializer(
           ApplicationDbContext context,
           RoleManager<IdentityRole> roleManager,
           UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        /// <summary>
        /// adds admin and shopper roles
        /// manually seed an Admin user and password
        /// </summary>
        /// <returns></returns>
        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }

            if (!_context.Roles.Any(r => r.Name == "Shopper"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Shopper" });
            }

            if (!_context.Users.Any(u => u.UserName == "admin@test.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "admin@user.com",
                    UserName = "admin@user.com"
                };
                await _userManager.CreateAsync(user, "Pass123!");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }

}
