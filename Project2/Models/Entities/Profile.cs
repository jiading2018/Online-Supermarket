using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Models.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "First Name is required"), MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required"), MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Street address is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Two digit state code is required"), StringLength(2)]
        public string State { get; set; }
        [Required(ErrorMessage = "Five digit zip code is required"), StringLength(5)]
        public string Zip { get; set; }

        public ApplicationUser User { get; set; }
        public ShoppingCart Cart { get; set; }
    }
}
