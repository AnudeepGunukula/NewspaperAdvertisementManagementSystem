using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewspaperAdvertisementManagementSystem.Models
{
    public class Client
    {

        [Key]
        public int ClientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string MobileNumber { get; set; }

        [NotMapped]
        public IFormFile ProfileImage { get; set; }





    }
}