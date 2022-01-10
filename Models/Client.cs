using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NewspaperAdvertisementManagementSystem.Models
{
    public class Client : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        // public string Email { get; set; }

        public string Address { get; set; }

        public string MobileNumber { get; set; }
        public string ProfileImageName { get; set; }

        [NotMapped]
        public IFormFile ProfileImageFile { get; set; }





    }
}