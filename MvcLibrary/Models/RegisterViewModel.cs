using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;
namespace MvcLibrary.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailAvailable", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select a Role")]
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        public SelectList? RoleOptions { get; set; }
    }
}