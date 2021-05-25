using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Views.Models
{
    public class SignupModel : PageModel
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(255)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(255)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(255)]
        public string Role { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
}
