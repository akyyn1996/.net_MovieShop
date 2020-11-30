using System;
using System.ComponentModel.DataAnnotations;

namespace MovieShop.Core.Models.Request
{
    public class UserRegisterRequestModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email  { get; set; }

        [Required]
        [StringLength(50,ErrorMessage = "Make Sure Password Is Right Length", MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password Should have minimum 8 with at least one upper, lower, number and special character")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }


        // create a custom class that derives from validation atrribute
    }
}