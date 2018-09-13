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
    /// Profile controler manages CRUD of profile
    /// 
    /// </summary>
    [Authorize]
    public class ProfileController : Controller
    {
        private IProfileRepository _profile;
        private IUserRepository _user;

        /// <summary>
        /// injection of the _profile repository and User repository
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="user"></param>
        public ProfileController(IProfileRepository profile, IUserRepository user)
        {
            _profile = profile;
            _user = user;
        }


        /// <summary>
        /// Action that returns a view to Create a _profile
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Create()
        {
            // check to make sure it is  valid user
            var profile = _user.ReadUser(User.Identity.Name).Profile;
            // only when a user does not have a profile can he/she create one
            if(profile != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        /// <summary>
        /// Post Create action method
        /// </summary>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Profile profile)
        {
            // Checks if the username is already used for a profile
            // if it is, display error message
            if (_profile.UsernameExists(profile.Username))                        
            {
                ModelState.AddModelError("Username", "Username is already taken!"); 
            }
            // if a username is not useed, and the profile is valid
            if (ModelState.IsValid)                                                  
            {
                // read the current user's email
                var user = _user.ReadUser(User.Identity.Name);
                profile.User = user;
                //create the profile
                _profile.CreateProfile(profile);
                //redirect to Home/Index
                return RedirectToAction("Index", "Home");                           
            }
            return View();                                                                                                           //if profile isn't valid, refresh view
        }


        /// <summary>
        /// Get Details action method
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IActionResult Details(string email)
        {
            // read the profile of the current user
            var profile = _profile.ReadProfileByEmail(email);
            // if profile is not found, redirect to Home/Index
            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // if profile is valid, display the view
            return View(profile);
        }


        /// <summary>
        /// GET Edit action method
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>a view with profile as model</returns>
        public IActionResult Edit(int id)
        {
            // read the profile based on id
            var profile = _profile.ReadProfile(id);
            // if profile is null, redirect to Home/Index
            if (profile == null)
            {
                return RedirectToAction("Index", "Profile");
            }
            // if profile exists, display the Edit view
            return View(profile);
        }


        /// <summary>
        /// POST Edit action method that updates a profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>View(profile)</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id, Email, Username, FirstName, LastName, Street, City, State, Zip")]Profile profile)
        {
            // checks to make sure model state is valid
            if (ModelState.IsValid)
            {
                // if valid, update profile with method from repository
                _profile.UpdateProfile(profile);
                // upon success, redirect to display ditails of new profile
                return RedirectToAction("Details", "Profile", profile.Email);
            }
           
            return View(profile);
            
        }

        /// <summary>
        /// GET Delete action method
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>view with profile as a model</returns>
        public IActionResult Delete(int id)
        {
            // read profile based on id
            var profile = _profile.ReadProfile(id);
            // if profile is null, redirect to Home/Index
            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // if profile is valid, display view
            return View(profile);
        }


        /// <summary>
        /// POST Delete action method that removes a profile from database
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>redirect to Home/Index</returns>
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // call repository method to delete profile
            _profile.DeleteProfile(id);
            // redirect to Home/Index
            return RedirectToAction("Index", "Home");
        }
    }
}