using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models.Entities;

namespace Project2.Services.Interfaces
{
    /// <summary>
    /// DbProfileRepository manages Profile Crud with database
    /// </summary>
    public class DbProfileRepository : IProfileRepository
    {
        private ApplicationDbContext _pro;
        /// <summary>
        /// inject IProfileRepository
        /// </summary>
        /// <param name="pro"></param>
        public DbProfileRepository(ApplicationDbContext pro)
        {
            _pro = pro;
        }


        /// <summary>
        /// CreateProfile
        /// add a new profile to table
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>Profile</returns>
        public Profile CreateProfile(Profile profile)
        {
            _pro.Profiles.Add(profile);
            _pro.SaveChanges();
            return profile;
        }


        /// <summary>
        /// DeleteProfile
        /// remove a profile from table with a given id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProfile(int id)
        {
            Profile profile = _pro.Profiles.FirstOrDefault(p => p.Id == id);
            _pro.Profiles.Remove(profile);
            _pro.SaveChanges();
        }


        /// <summary>
        /// ReadProfile
        /// read an return a profile with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Profile</returns>
        public Profile ReadProfile(int id)
        {
            var profile = _pro.Profiles.FirstOrDefault(p=> p.Id == id);
            return profile;
        }


        /// <summary>
        /// ReaduserName
        /// read and return a profile with a given user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Profile</returns>
        public Profile ReadUsername(string username)
        {
            var profile = _pro.Profiles.FirstOrDefault(p => p.Username == username);
            return profile;
        }


        /// <summary>
        /// Read and return a profile with a given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Profile</returns>
        public Profile ReadProfileByEmail(string email)
        {
            var profile = _pro.Profiles.FirstOrDefault(p => p.Email == email);
            return profile;
        }


        /// <summary>
        /// UsernameExists
        /// check to see if a profile exists with a given username
        /// if profile exists, return true, if not return false
        /// </summary>
        /// <param name="username"></param>
        /// <returns>bool</returns>
        public bool UsernameExists(string username)
        {
            var profile = _pro.Profiles.FirstOrDefault(p => p.Username == username);
            if (profile == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        

        /// <summary>
        /// UpdateProfile
        /// update a profile with a given new profile
        /// </summary>
        /// <param name="profile"></param>
        public void UpdateProfile(Profile profile)
        {
            _pro.Entry(profile).State = EntityState.Modified;
            _pro.SaveChanges();
        }
    }
}
