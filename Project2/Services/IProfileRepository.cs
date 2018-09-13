using Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services.Interfaces
{
    /// <summary>
    /// IProfileRepository creates CRUD interface for Profile
    /// </summary>
    public interface IProfileRepository
    {
        /// <summary>
        /// CreateProfile
        /// add a new profile to table
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>Profile</returns>
        Profile CreateProfile(Profile profile);
        /// <summary>
        /// ReadProfile
        /// read an return a profile with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Profile</returns>
        Profile ReadProfile(int id);
        /// <summary>
        /// UpdateProfile
        /// update a profile with a given new profile
        /// </summary>
        /// <param name="profile"></param>
        void UpdateProfile(Profile profile);
        /// <summary>
        /// DeleteProfile
        /// remove a profile from table with a given id
        /// </summary>
        /// <param name="id"></param>
        void DeleteProfile(int id);
        /// <summary>
        /// Read and return a profile with a given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Profile</returns>
        Profile ReadProfileByEmail(string email);
        /// <summary>
        /// ReaduserName
        /// read and return a profile with a given user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Profile</returns>
        Profile ReadUsername(string username);
        /// <summary>
        /// UsernameExists
        /// check to see if a profile exists with a given username
        /// if profile exists, return true, if not return false
        /// </summary>
        /// <param name="username"></param>
        /// <returns>bool</returns>
        bool UsernameExists(string username);
    }
}
